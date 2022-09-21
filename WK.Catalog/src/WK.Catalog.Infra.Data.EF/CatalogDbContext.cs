using WK.Catalog.Infra.Data.EF.Configurations;

using Microsoft.EntityFrameworkCore;
using WK.Catalog.Infra.Data.EF.Models;

namespace WK.Catalog.Infra.Data.EF;
public class CatalogDbContext
    : DbContext
{
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public CatalogDbContext(
        DbContextOptions<CatalogDbContext> options
    ) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging();
}
