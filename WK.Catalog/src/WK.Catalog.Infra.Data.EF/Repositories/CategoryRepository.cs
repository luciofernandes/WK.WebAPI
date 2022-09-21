using WK.Catalog.Application.Exceptions;
using WK.Catalog.Domain.Entity;
using WK.Catalog.Domain.Repository;
using WK.Catalog.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;
using Model = WK.Catalog.Infra.Data.EF.Models;
using WK.Catalog.Infra.Data.EF.Models.Extensions;

namespace WK.Catalog.Infra.Data.EF.Repositories;
public class CategoryRepository
    : ICategoryRepository
{
    private readonly CatalogDbContext _context;
    private DbSet<Model.Category> _categories 
        => _context.Set<Model.Category>();

    public CategoryRepository(CatalogDbContext context) 
        => _context = context;

    public async Task Insert(
        Category aggregate, 
        CancellationToken cancellationToken
    )
     => (await _categories.AddAsync(aggregate.ToModel(), cancellationToken)).Entity.ToEntity();
  
    public async Task<Category> Get(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categories.AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == id,
            cancellationToken
        );
        NotFoundException.ThrowIfNull(category, $"Category '{id}' not found.");
        return category!.ToEntity();
    }

    public Task Update(Category aggregate, CancellationToken _)
        => Task.FromResult(_categories.Update(aggregate.ToModel()));

    public Task Delete(Category aggregate, CancellationToken _)
        => Task.FromResult(_categories.Remove(aggregate.ToModel()));

    public async Task<SearchOutput<Category>> Search(
        SearchInput input, 
        CancellationToken cancellationToken)
    {
        var toSkip = (input.Page - 1) * input.PerPage;
        var query = _categories.AsNoTracking();
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

    private IQueryable<Models.Category> AddOrderToQuery(
        IQueryable<Models.Category> query,
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
        => await _categories.AsNoTracking()
            .Where(category => ids.Contains(category.Id))
            .Select(category => category.Id).ToListAsync();

    public async Task<IReadOnlyList<Category>> GetListByIds(List<Guid> ids, CancellationToken cancellationToken)
        => (await _categories.AsNoTracking()
            .Where(category => ids.Contains(category.Id))
            .ToListAsync()).Select(i => i.ToEntity()).ToList();
}
