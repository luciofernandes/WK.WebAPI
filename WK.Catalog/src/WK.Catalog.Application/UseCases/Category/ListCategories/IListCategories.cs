using MediatR;

namespace WK.Catalog.Application.UseCases.Category.ListCategories;
public interface IListCategories
    : IRequestHandler<ListCategoriesInput, ListCategoriesOutput>
{}
