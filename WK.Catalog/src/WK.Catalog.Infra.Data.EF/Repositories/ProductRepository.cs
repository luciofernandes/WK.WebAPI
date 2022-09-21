using WK.Catalog.Application.Exceptions;
using WK.Catalog.Domain.Entity;
using WK.Catalog.Domain.Repository;
using WK.Catalog.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;
using WK.Catalog.Infra.Data.EF.Models.Extensions;
using System.Threading;

namespace WK.Catalog.Infra.Data.EF.Repositories;
public class ProductRepository
    : IProductRepository
{
    private readonly CatalogDbContext _context;
    private DbSet<Models.Product> _products 
        => _context.Set<Models.Product>();

    public ProductRepository(CatalogDbContext context) 
        => _context = context;

    public async Task Insert(
        Product aggregate, 
        CancellationToken cancellationToken
    )
        => (await _products.AddAsync(aggregate.ToModel(), cancellationToken)).Entity.ToEntity();

    public async Task<Product> Get(Guid id, CancellationToken cancellationToken)
    {
        var product = await _products.Include(p=>p.Category).AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );
        NotFoundException.ThrowIfNull(product, $"Product '{id}' not found.");
        return product!.ToEntity();
    }

    public Task Update(Product aggregate, CancellationToken _)
        => Task.FromResult(_products.Update(aggregate.ToModel()));

    public Task Delete(Product aggregate, CancellationToken _)
        => Task.FromResult(_products.Remove(aggregate.ToModel()));

    public async Task<SearchOutput<Product>> Search(
        SearchInput input, 
        CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _products.Include(p => p.Category).AsNoTracking();
        query = AddOrderToQuery(query, input.OrderBy, input.Order);
        if(!String.IsNullOrWhiteSpace(input.Search))
            query = query.Where(x => x.Name!.Contains(input.Search));
        var total = await query.CountAsync();
        var items = await query
            .Skip(toSkip)
            .Take(input.PerPage)
            .ToListAsync();
        return new(input.Page, input.PerPage, total, items.Select(i => i.ToEntity()).ToList());
    }

    private IQueryable<Models.Product> AddOrderToQuery(
        IQueryable<Models.Product> query,
        string orderProperty,
        SearchOrder order
    )
    { 
        var orderedQuery = (orderProperty.ToLower(), order) switch
        {
            ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id),
            ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Id),
            ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
            ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
            _ => query.OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
        };
        return orderedQuery;
    }

    public async Task<IReadOnlyList<Guid>> GetIdsListByIds(
        List<Guid> ids, 
        CancellationToken cancellationToken
    ) 
        => await _products.Include(p => p.Category).AsNoTracking()
            .Where(product => ids.Contains(product.Id))
            .Select(product => product.Id).ToListAsync();

    public async Task<IReadOnlyList<Product>> GetListByIds(List<Guid> ids, CancellationToken cancellationToken)
        => (await _products.Include(p => p.Category).AsNoTracking()
            .Where(product => ids.Contains(product.Id))
            .ToListAsync()).Select(i => i.ToEntity()).ToList();

}
