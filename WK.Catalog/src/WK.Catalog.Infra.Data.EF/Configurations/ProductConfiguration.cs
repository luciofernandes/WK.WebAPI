using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata;
using WK.Catalog.Infra.Data.EF.Models;

namespace WK.Catalog.Infra.Data.EF.Configurations;
public class ProductConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(product => product.Id)
            .HasName("PK_Product");
        builder.Property(product => product.Name).IsRequired();
 
    }
}