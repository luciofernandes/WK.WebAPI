using Xunit;
using DomainEntity = WK.Catalog.Domain.Entity;
using WK.Catalog.UnitTests.Common;

namespace WK.Catalog.UnitTests.Domain.Entity.Category
{
    public class CategoryTestFixture : BaseFixture
    {
        public DomainEntity.Category GetValidCategory()
            => new DomainEntity.Category(Faker.Commerce.Categories(1)[0]);
    }

    [CollectionDefinition(nameof(CategoryTestFixture))]
    public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture>
    { }
}
