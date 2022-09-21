using UseCase = WK.Catalog.Application.UseCases.Category.GetCategory;
using WK.Catalog.Infra.Data.EF.Repositories;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using FluentAssertions;
using System;
using WK.Catalog.Application.Exceptions;
using WK.Catalog.Infra.Data.EF.Models.Extensions;

namespace WK.Catalog.IntegrationTests.Application.UseCases.Category.GetCategory;

[Collection(nameof(GetCategoryTestFixture))]
public class GetCategoryTest
{
    private readonly GetCategoryTestFixture _fixture;

    public GetCategoryTest(GetCategoryTestFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(GetCategory))]
    [Trait("Integration/Application", "GetCategory - Use Cases")]
    public async Task GetCategory()
    {
        var exampleCategory = _fixture.GetExampleCategory();
        var dbContext = _fixture.CreateDbContext();
        dbContext.Categories.Add(exampleCategory.ToModel());
        dbContext.SaveChanges();
        var repository = new CategoryRepository(dbContext);
        var input = new UseCase.GetCategoryInput(exampleCategory.Id);
        var useCase = new UseCase.GetCategory(repository);

        var output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(exampleCategory.Name);
        output.Id.Should().Be(exampleCategory.Id);
    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenCategoryDoesntExist))]
    [Trait("Integration/Application", "GetCategory - Use Cases")]
    public async Task NotFoundExceptionWhenCategoryDoesntExist()
    {
        var exampleCategory = _fixture.GetExampleCategory();
        var dbContext = _fixture.CreateDbContext();
        dbContext.Categories.Add(exampleCategory.ToModel());
        dbContext.SaveChanges();
        var repository = new CategoryRepository(dbContext);
        var input = new UseCase.GetCategoryInput(Guid.NewGuid());
        var useCase = new UseCase.GetCategory(repository);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Category '{input.Id}' not found.");
    }
}
