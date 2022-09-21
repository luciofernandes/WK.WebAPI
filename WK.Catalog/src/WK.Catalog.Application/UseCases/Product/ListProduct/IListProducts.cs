using MediatR;

namespace WK.Catalog.Application.UseCases.Product.ListProducts;
public interface IListProducts
    : IRequestHandler<ListProductsInput, ListProductsOutput>
{}
