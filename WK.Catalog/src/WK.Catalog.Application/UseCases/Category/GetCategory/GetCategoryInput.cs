using WK.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace WK.Catalog.Application.UseCases.Category.GetCategory;
public class GetCategoryInput : IRequest<CategoryModelOutput>
{
    public Guid Id { get; set; }
    public GetCategoryInput(Guid id) 
        => Id = id;
}
