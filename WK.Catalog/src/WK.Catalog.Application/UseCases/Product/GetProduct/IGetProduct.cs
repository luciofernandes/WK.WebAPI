
using MediatR;
using WK.Catalog.Application.UseCases.Product.Common;

namespace WK.Catalog.Application.UseCases.Product.GetProduct;
public interface IGetProduct : IRequestHandler<GetProductInput, ProductModelOutput>
{}
