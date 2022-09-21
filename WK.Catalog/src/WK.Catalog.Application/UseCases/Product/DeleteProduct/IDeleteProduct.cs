using MediatR;

namespace WK.Catalog.Application.UseCases.Product.DeleteProduct;
public interface IDeleteProduct
    : IRequestHandler<DeleteProductInput>
{}
