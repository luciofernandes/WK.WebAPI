using WK.Catalog.Application.Common;
using WK.Catalog.Domain.SeedWork.SearchableRepository;
using MediatR;

namespace WK.Catalog.Application.UseCases.Product.ListProducts;
public class ListProductsInput
    : PaginatedListInput, IRequest<ListProductsOutput>
{
    public ListProductsInput(
        int page = 1,
        int perPage = 15,
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc
    ) : base(page, perPage, search, sort, dir)
    { }
    
    public ListProductsInput() 
        : base(1, 15, "", "", SearchOrder.Asc)
    { }
}
