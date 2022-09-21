
using MediatR;
using WK.Catalog.Application.UseCases.Product.Common;

namespace WK.Catalog.Application.UseCases.Product.GetProduct;
public class GetProductInput : IRequest<ProductModelOutput>
{
    public Guid Id { get; set; }
    public GetProductInput(Guid id) 
        => Id = id;
}
