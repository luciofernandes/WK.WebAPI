using WK.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace WK.Catalog.Application.UseCases.Category.GetCategory;
public interface IGetCategory: IRequestHandler<GetCategoryInput, CategoryModelOutput>
{}
