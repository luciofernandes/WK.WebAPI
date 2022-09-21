using WK.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace WK.Catalog.Application.UseCases.Category.CreateCategory;
public interface ICreateCategory : 
    IRequestHandler<CreateCategoryInput, CategoryModelOutput>
{ }
