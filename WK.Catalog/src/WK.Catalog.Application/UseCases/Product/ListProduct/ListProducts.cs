
using WK.Catalog.Application.UseCases.Product.Common;

using WK.Catalog.Domain.Repository;

namespace WK.Catalog.Application.UseCases.Product.ListProducts;
public class ListProducts : IListProducts
{
    private readonly IProductRepository _productRepository;

    public ListProducts(IProductRepository productRepository) 
        => _productRepository = productRepository;

    public async Task<ListProductsOutput> Handle(
        ListProductsInput request, 
        CancellationToken cancellationToken)
    {
        var searchOutput = await _productRepository.Search(
            new(
                request.Page, 
                request.PerPage, 
                request.Search, 
                request.Sort, 
                request.Dir
            ),
            cancellationToken
        );

        return new ListProductsOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
        searchOutput.Total,
        searchOutput.Items
                .Select(ProductModelOutput.FromProduct)
                .ToList()
        );
    }
}
