using WK.Catalog.Application.UseCases.Category.CreateCategory;
using WK.Catalog.UnitTests.Application.Category.Common;
using Xunit;

namespace WK.Catalog.UnitTests.Application.Category.CreateCategory;

[CollectionDefinition(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTestFixtureCollection
    : ICollectionFixture<CreateCategoryTestFixture>
{ }

public class CreateCategoryTestFixture
    : CategoryUseCasesBaseFixture
{
    public CreateCategoryInput GetInput()
        => new(
            GetValidCategoryName()
        );

    

}
