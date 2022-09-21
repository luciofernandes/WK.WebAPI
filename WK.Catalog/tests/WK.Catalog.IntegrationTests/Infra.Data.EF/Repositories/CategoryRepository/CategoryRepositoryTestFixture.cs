using WK.Catalog.Domain.Entity;
using WK.Catalog.Domain.SeedWork.SearchableRepository;
using WK.Catalog.IntegrationTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace WK.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository;

[CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
public class CategoryRepositoryTestFixtureCollection
    : ICollectionFixture<CategoryRepositoryTestFixture>
{}

public class CategoryRepositoryTestFixture
    : BaseFixture
{
    public string GetValidCategoryName()
    {
        var categoryName = Faker.Commerce.Categories(1)[0];
        return categoryName;
    }


    public bool getRandomBoolean()
        => new Random().NextDouble() < 0.5;

    public Category GetExampleCategory()
        => new(
            GetValidCategoryName()
        );

    public List<Category> GetExampleCategoriesList(int length = 10)
        => Enumerable.Range(1, length)
            .Select(_ => GetExampleCategory()).ToList();

    public List<Category> GetExampleCategoriesListWithNames(List<string> names)
        => names.Select(name =>
        {
            var category = GetExampleCategory();
            category.Update(name);
            return category;
        }).ToList();

    public List<Category> CloneCategoriesListOrdered(
        List<Category> categoriesList,
        string orderBy,
        SearchOrder order
    )
    {
        var listClone = new List<Category>(categoriesList);
        var orderedEnumerable = (orderBy.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => listClone.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => listClone.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => listClone.OrderByDescending(x => x.Id),
            _ => listClone.OrderBy(x => x.Name).ThenBy(x => x.Id),
        };
        return orderedEnumerable.ToList();
    }
}
