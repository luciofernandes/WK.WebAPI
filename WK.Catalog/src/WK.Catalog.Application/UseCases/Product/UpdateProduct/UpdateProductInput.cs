using WK.Catalog.Application.UseCases.Product.Common;
using DomainEntity = WK.Catalog.Domain.Entity;
using MediatR;
using WK.Catalog.Application.UseCases.Category.UpdateCategory;

namespace WK.Catalog.Application.UseCases.Product.UpdateProduct;
public class UpdateProductInput
    : IRequest<ProductModelOutput>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public string Description { get; set; }

    public Guid CategoryId { get; set; }

    public UpdateProductInput(
        Guid id, 
        string name,
        string description,
        Guid categoryId )
    {
        Id = id;
        Name = name;
        Description = description;
        CategoryId = categoryId;
    }

}
