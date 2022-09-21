using WK.Catalog.Domain.Entity;
using WK.Catalog.Domain.SeedWork;
using WK.Catalog.Domain.SeedWork.SearchableRepository;

namespace WK.Catalog.Domain.Repository;
public interface ICategoryRepository 
    : IGenericRepository<Category>,
    ISearchableRepository<Category>
{
    public Task<IReadOnlyList<Guid>> GetIdsListByIds(
        List<Guid> ids,
        CancellationToken cancellationToken
    );

    public Task<IReadOnlyList<Category>> GetListByIds(
        List<Guid> ids,
        CancellationToken cancellationToken
    );
}
