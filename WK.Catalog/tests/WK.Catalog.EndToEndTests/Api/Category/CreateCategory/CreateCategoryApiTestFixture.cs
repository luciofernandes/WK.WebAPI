using WK.Catalog.Application.UseCases.Category.CreateCategory;
using WK.Catalog.EndToEndTests.Api.Category.Common;
using Xunit;

namespace WK.Catalog.EndToEndTests.Api.Category.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryApiTestFixture))]
public class CreateCategoryApiTestFixtureCollection 
    : ICollectionFixture<CreateCategoryApiTestFixture>
{ }

public class CreateCategoryApiTestFixture
    : CategoryBaseFixture
{
    public CreateCategoryInput getExampleInput()
        => new(
            GetValidCategoryName()
        );
}
