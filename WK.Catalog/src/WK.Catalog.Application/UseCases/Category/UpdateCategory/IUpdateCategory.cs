using WK.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace WK.Catalog.Application.UseCases.Category.UpdateCategory;
public interface IUpdateCategory
    : IRequestHandler<UpdateCategoryInput, CategoryModelOutput>
{}
