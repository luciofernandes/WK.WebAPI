
using Microsoft.EntityFrameworkCore;
using System.Threading;
using FluentAssertions;
using System.Threading.Tasks;
using Xunit;
using WK.Catalog.Infra.Data.EF;
using Repository = WK.Catalog.Infra.Data.EF.Repositories;
using System;
using WK.Catalog.Application.Exceptions;
using WK.Catalog.Domain.SeedWork.SearchableRepository;
using WK.Catalog.Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace WK.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;

[Collection(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTest
{
    private readonly CategoryRepositoryTestFixture _fixture;

    public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(Insert))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task Insert()
    {
        CatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();
        var categoryRepository = new Repository.CategoryRepository(dbContext);

        await categoryRepository.Insert(exampleCategory, CancellationToken.None);
        await dbContext.SaveChangesAsync(CancellationToken.None);

        var dbCategory = await (_fixture.CreateDbContext(true))
            .Categories.FindAsync(exampleCategory.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(exampleCategory.Name);
        
    }

    [Fact(DisplayName = nameof(Get))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task Get()
    {
        CatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();
        var exampleCategoriesList = _fixture.GetExampleCategoriesList(15);
        exampleCategoriesList.Add(exampleCategory);
        await dbContext.AddRangeAsync(exampleCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(
            _fixture.CreateDbContext(true)
        );

        var dbCategory = await categoryRepository.Get(
            exampleCategory.Id, 
            CancellationToken.None);

        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(exampleCategory.Name);
        dbCategory.Id.Should().Be(exampleCategory.Id);
    }

    [Fact(DisplayName = nameof(GetThrowIfNotFound))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task GetThrowIfNotFound()
    {
        CatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleId = Guid.NewGuid();
        await dbContext.AddRangeAsync(_fixture.GetExampleCategoriesList(15));
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);

        var task = async () => await categoryRepository.Get(
            exampleId,
            CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{exampleId}' not found.");
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task Update()
    {
        CatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();
        var newCategoryValues = _fixture.GetExampleCategory();
        var exampleCategoriesList = _fixture.GetExampleCategoriesList(15);
        exampleCategoriesList.Add(exampleCategory);
        await dbContext.AddRangeAsync(exampleCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);

        exampleCategory.Update(newCategoryValues.Name);
        await categoryRepository.Update(exampleCategory, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        var dbCategory = await (_fixture.CreateDbContext(true))
            .Categories.FindAsync(exampleCategory.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(exampleCategory.Name);
        dbCategory.Id.Should().Be(exampleCategory.Id);
       
    }

    [Fact(DisplayName = nameof(Delete))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task Delete()
    {
        CatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategory = _fixture.GetExampleCategory();
        var exampleCategoriesList = _fixture.GetExampleCategoriesList(15);
        exampleCategoriesList.Add(exampleCategory);
        await dbContext.AddRangeAsync(exampleCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);

        await categoryRepository.Delete(exampleCategory, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        var dbCategory = await (_fixture.CreateDbContext(true))
            .Categories.FindAsync(exampleCategory.Id);
        dbCategory.Should().BeNull();
    }

    [Fact(DisplayName = nameof(ListByIds))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task ListByIds()
    {
        CatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategoriesList = _fixture.GetExampleCategoriesList(15);
        List<Guid> categoriesIdsToGet = Enumerable.Range(1, 3).Select(_ => {
            int indexToGet = (new Random()).Next(0, exampleCategoriesList.Count - 1);
            return exampleCategoriesList[indexToGet].Id;
        }).Distinct().ToList();
        await dbContext.AddRangeAsync(exampleCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);
        IReadOnlyList<Category> categoriesList = await categoryRepository
            .GetListByIds(categoriesIdsToGet, CancellationToken.None);

        categoriesList.Should().NotBeNull();
        categoriesList.Should().HaveCount(categoriesIdsToGet.Count);
        foreach (Category outputItem in categoriesList)
        {
            var exampleItem = exampleCategoriesList.Find(
                category => category.Id == outputItem.Id
            );
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem!.Name);
        }
    }

    [Fact(DisplayName = nameof(SearchRetursListAndTotal))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    public async Task SearchRetursListAndTotal()
    {
        CatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategoriesList = _fixture.GetExampleCategoriesList(15);
        await dbContext.AddRangeAsync(exampleCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);
        var searchInput = new SearchInput(1, 20, "", "", SearchOrder.Asc);

        var output = await categoryRepository.Search(searchInput, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.CurrentPage.Should().Be(searchInput.Page);
        output.PerPage.Should().Be(searchInput.PerPage);
        output.Total.Should().Be(exampleCategoriesList.Count);
        output.Items.Should().HaveCount(exampleCategoriesList.Count);
        foreach(Category outputItem in output.Items)
        {
            var exampleItem = exampleCategoriesList.Find(
                category => category.Id == outputItem.Id
            );
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem!.Name);
        }
    }

    [Theory(DisplayName = nameof(SearchByText))]
    [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
    [InlineData("Action", 1, 5, 1, 1)]
    [InlineData("Horror", 1, 5, 3, 3)]
    [InlineData("Horror", 2, 5, 0, 3)]
    [InlineData("Sci-fi", 1, 5, 4, 4)]
    [InlineData("Sci-fi", 1, 2, 2, 4)]
    [InlineData("Sci-fi", 2, 3, 1, 4)]
    [InlineData("Sci-fi Other", 1, 3, 0, 0)]
    [InlineData("Robots", 1, 5, 2, 2)]
    public async Task SearchByText(
        string search,
        int page,
        int perPage,
        int expectedQuantityItemsReturned,
        int expectedQuantityTotalItems
    )
    {
        CatalogDbContext dbContext = _fixture.CreateDbContext();
        var exampleCategoriesList =
            _fixture.GetExampleCategoriesListWithNames(new List<string>() { 
                "Action",
                "Horror",
                "Horror - Robots",
                "Horror - Based on Real Facts",
                "Drama",
                "Sci-fi IA",
                "Sci-fi Space",
                "Sci-fi Robots",
                "Sci-fi Future"
            });
        await dbContext.AddRangeAsync(exampleCategoriesList);
        await dbContext.SaveChangesAsync(CancellationToken.None);
        var categoryRepository = new Repository.CategoryRepository(dbContext);
        var searchInput = new SearchInput(page, perPage, search, "", SearchOrder.Asc);

        var output = await categoryRepository.Search(searchInput, CancellationToken.None);

        output.Should().NotBeNull();
        output.Items.Should().NotBeNull();
        output.CurrentPage.Should().Be(searchInput.Page);
        output.PerPage.Should().Be(searchInput.PerPage);
        output.Total.Should().Be(expectedQuantityTotalItems);
        output.Items.Should().HaveCount(expectedQuantityItemsReturned);
        foreach (Category outputItem in output.Items)
        {
            var exampleItem = exampleCategoriesList.Find(
                category => category.Id == outputItem.Id
            );
            exampleItem.Should().NotBeNull();
            outputItem.Name.Should().Be(exampleItem!.Name);
        }
    }

   
}
