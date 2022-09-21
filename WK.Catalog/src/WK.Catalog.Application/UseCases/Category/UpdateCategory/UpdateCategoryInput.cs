using WK.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace WK.Catalog.Application.UseCases.Category.UpdateCategory;
public class UpdateCategoryInput 
    : IRequest<CategoryModelOutput>
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public UpdateCategoryInput(
        Guid id, 
        string name)
    {
        Id = id;
        Name = name;
    }

}
