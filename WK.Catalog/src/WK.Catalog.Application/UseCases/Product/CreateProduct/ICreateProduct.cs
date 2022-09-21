
using MediatR;

using WK.Catalog.Application.UseCases.Product.Common;

namespace WK.Catalog.Application.UseCases.Product.CreateProduct;
public interface ICreateProduct : 
    IRequestHandler<CreateProductInput, ProductModelOutput>
{ }
