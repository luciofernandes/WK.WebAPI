using WK.Catalog.Application.UseCases.Category.Common;
using DomainEntity = WK.Catalog.Domain.Entity;

namespace WK.Catalog.Application.UseCases.Product.Common;
public class ProductModelOutput
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public CategoryModelOutput Category { get; set; }
    public ProductModelOutput(
        Guid id,
        string name,
        string description,
        CategoryModelOutput category
    )
    {
        Id = id;
        Name = name;
        Description = description;
        Category = category;

    }

    public static ProductModelOutput FromProduct(DomainEntity.Product product)
    {
        var category = new CategoryModelOutput(
            product.Category.Id,
            product.Category.Name
            );

        return new ProductModelOutput ( product.Id,
            product.Name,
            product.Description,
            category
        );
    }
        
}
