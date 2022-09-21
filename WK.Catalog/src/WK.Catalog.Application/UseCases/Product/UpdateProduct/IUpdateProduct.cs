using WK.Catalog.Application.UseCases.Product.Common;
using MediatR;

namespace WK.Catalog.Application.UseCases.Product.UpdateProduct;
public interface IUpdateProduct
    : IRequestHandler<UpdateProductInput, ProductModelOutput>
{}
