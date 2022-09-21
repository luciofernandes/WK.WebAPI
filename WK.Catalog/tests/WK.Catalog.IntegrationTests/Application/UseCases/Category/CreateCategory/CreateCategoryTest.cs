using WK.Catalog.Infra.Data.EF;
using WK.Catalog.Infra.Data.EF.Repositories;
using ApplicationUseCases = WK.Catalog.Application.UseCases.Category.CreateCategory;
using Xunit;
using System.Threading;
using FluentAssertions;
using WK.Catalog.Application.UseCases.Category.CreateCategory;
using WK.Catalog.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WK.Catalog.IntegrationTests.Application.UseCases.Category.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Integration/Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var dbContext = _fixture.CreateDbContext();
        var repository = new CategoryRepository(dbContext);
        var unitOfWor = new UnitOfWork(dbContext);
        var useCase = new ApplicationUseCases.CreateCategory(
            repository, unitOfWor
        );
        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        var dbCategory = await (_fixture.CreateDbContext(true))
            .Categories.FindAsync(output.Id);
        dbCategory.Should().NotBeNull();
        dbCategory!.Name.Should().Be(input.Name);
      
        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Id.Should().NotBeEmpty();
    }

}
