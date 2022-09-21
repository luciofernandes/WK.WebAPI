
using WK.Catalog.Domain.Entity;
using WK.Catalog.Domain.SeedWork.SearchableRepository;
using WK.Catalog.Domain.SeedWork;

namespace WK.Catalog.Domain.Repository
{
    public interface IProductRepository : IGenericRepository<Product>,
    ISearchableRepository<Product>
    {
        public Task<IReadOnlyList<Guid>> GetIdsListByIds(
            List<Guid> ids,
            CancellationToken cancellationToken
        );

        public Task<IReadOnlyList<Product>> GetListByIds(
            List<Guid> ids,
            CancellationToken cancellationToken
        );
    }
}
