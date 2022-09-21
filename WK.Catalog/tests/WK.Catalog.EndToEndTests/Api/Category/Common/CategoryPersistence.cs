using WK.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DomainEntity = WK.Catalog.Domain.Entity;

namespace WK.Catalog.EndToEndTests.Api.Category.Common;
public class CategoryPersistence
{
    private readonly CatalogDbContext _context;

    public CategoryPersistence(CatalogDbContext context)
        => _context = context;

    public async Task<DomainEntity.Category?> GetById(Guid id)
        => await _context
            .Categories.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task InsertList(List<DomainEntity.Category> categories)
    {
        await _context.Categories.AddRangeAsync(categories);
        await _context.SaveChangesAsync();
    }
}
