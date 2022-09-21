using Bogus;
using WK.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace WK.Catalog.IntegrationTests.Base;
public class BaseFixture
{
    public BaseFixture() 
        => Faker = new Faker("pt_BR");

    protected Faker Faker { get; set; }

    public CatalogDbContext CreateDbContext(bool preserveData = false)
    {
        var context = new CatalogDbContext(
            new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase("integration-tests-db")
            .Options
        );
        if (preserveData == false)
            context.Database.EnsureDeleted();
        return context;
    }
}
