using MediatR;

namespace WK.Catalog.Application.UseCases.Category.DeleteCategory;
public interface IDeleteCategory 
    : IRequestHandler<DeleteCategoryInput>
{}
