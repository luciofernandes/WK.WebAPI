using WK.Catalog.Application.Common;
using WK.Catalog.Application.UseCases.Product.Common;

namespace WK.Catalog.Application.UseCases.Product.ListProducts;
public class ListProductsOutput
    : PaginatedListOutput<ProductModelOutput>
{
    public ListProductsOutput(
        int page, 
        int perPage, 
        int total, 
        IReadOnlyList<ProductModelOutput> items) 
        : base(page, perPage, total, items)
    {
    }
}
