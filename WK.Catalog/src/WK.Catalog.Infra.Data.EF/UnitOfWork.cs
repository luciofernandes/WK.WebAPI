using WK.Catalog.Application.Interfaces;

namespace WK.Catalog.Infra.Data.EF;
public class UnitOfWork
    : IUnitOfWork
{
    private readonly CatalogDbContext _context;

    public UnitOfWork(CatalogDbContext context)
        => _context = context;

    public Task Commit(CancellationToken cancellationToken)
        => _context.SaveChangesAsync(cancellationToken);

    public Task Rollback(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
