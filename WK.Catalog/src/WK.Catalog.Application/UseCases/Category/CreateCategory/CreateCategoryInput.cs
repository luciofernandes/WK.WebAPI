using WK.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace WK.Catalog.Application.UseCases.Category.CreateCategory;
public class CreateCategoryInput : IRequest<CategoryModelOutput>
{
    public string Name { get; set; }

    public CreateCategoryInput(
        string name
       )
    {
        Name = name;
    }
}
