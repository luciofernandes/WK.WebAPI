using WK.Catalog.Api.ApiModels.Category;
using WK.Catalog.EndToEndTests.Api.Category.Common;
using Xunit;

namespace WK.Catalog.EndToEndTests.Api.Category.UpdateCategory;

[CollectionDefinition(nameof(UpdateCategoryApiTestFixture))]
public class UpdateCategoryApiTestFixtureCollection
    : ICollectionFixture<UpdateCategoryApiTestFixture>
{ }

public class UpdateCategoryApiTestFixture
    : CategoryBaseFixture
{
    public UpdateCategoryApiInput GetExampleInput()
        => new (
            GetValidCategoryName()
        );
}
