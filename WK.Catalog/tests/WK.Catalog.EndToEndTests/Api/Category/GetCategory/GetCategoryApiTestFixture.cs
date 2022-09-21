using WK.Catalog.Application.UseCases.Category.GetCategory;
using WK.Catalog.EndToEndTests.Api.Category.Common;
using Xunit;

namespace WK.Catalog.EndToEndTests.Api.Category.GetCategoryById;

[CollectionDefinition(nameof(GetCategoryApiTestFixture))]
public class GetCategoryApiTestFixtureCollection
    : ICollectionFixture<GetCategoryApiTestFixture>
{ }

public class GetCategoryApiTestFixture
    : CategoryBaseFixture
{ }
