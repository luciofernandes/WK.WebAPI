using WK.Catalog.Application.Interfaces;
using WK.Catalog.Application.UseCases.Product.Common;
using WK.Catalog.Domain.Entity;
using WK.Catalog.Domain.Repository;

namespace WK.Catalog.Application.UseCases.Product.UpdateProduct;
public class UpdateProduct : IUpdateProduct
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _uinitOfWork;

    public UpdateProduct(
        IProductRepository productRepository, 
        ICategoryRepository categoryRepository,
        IUnitOfWork uinitOfWork)
        => (_productRepository, _categoryRepository, _uinitOfWork) 
            = (productRepository, categoryRepository, uinitOfWork);

    public async Task<ProductModelOutput> Handle(UpdateProductInput request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.Get(request.Id, cancellationToken);
        var category = await _categoryRepository.Get(request.CategoryId, cancellationToken);
        product.Update(request.Name,request.Description, category);
        await _productRepository.Update(product, cancellationToken);
        await _uinitOfWork.Commit(cancellationToken);
        return ProductModelOutput.FromProduct(product);
    }
}
