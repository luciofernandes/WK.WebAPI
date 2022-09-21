using WK.Catalog.Application.UseCases.Product.Common;
using WK.Catalog.Domain.Repository;

namespace WK.Catalog.Application.UseCases.Product.GetProduct;
public class GetProduct : IGetProduct
{
    private readonly IProductRepository _productRepository;

    public GetProduct(IProductRepository productRepository) 
        => _productRepository = productRepository;

    public async Task<ProductModelOutput> Handle(
        GetProductInput request, 
        CancellationToken cancellationToken
    )
    {
        var product = await _productRepository.Get(request.Id, cancellationToken);
        return ProductModelOutput.FromProduct(product);
    }
}
