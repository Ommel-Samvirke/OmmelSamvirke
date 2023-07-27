using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class PageRepository : GenericRepository<Page>, IPageRepository
{
    public PageRepository(AppDbContext dbDbContext) : base(dbDbContext) { }
    
    public override async Task<IReadOnlyList<Page>> GetWithRelationsAsync(CancellationToken cancellationToken = default)
    {
        IQueryable<Page> pagesWithRelations = DbSet
            .Include(p => p.DesktopConfiguration)
            .ThenInclude(d => d.ContentBlocks)
            .Include(p => p.TabletConfiguration)
            .ThenInclude(t => t.ContentBlocks)
            .Include(p => p.MobileConfiguration)
            .ThenInclude(m => m.ContentBlocks)
            .AsNoTracking();

        return await pagesWithRelations.ToListAsync(cancellationToken);
    }
    
    public override async Task<Page?> GetByIdWithRelationsAsync(int id, CancellationToken cancellationToken = default)
    {
        IQueryable<Page?> pageWithRelations = DbSet
            .Include(p => p.DesktopConfiguration)
            .ThenInclude(d => d.ContentBlocks)
            .Include(p => p.TabletConfiguration)
            .ThenInclude(t => t.ContentBlocks)
            .Include(p => p.MobileConfiguration)
            .ThenInclude(m => m.ContentBlocks)
            .AsNoTracking()
            .Where(p => p.Id == id);

        return await pageWithRelations.FirstOrDefaultAsync(cancellationToken);
    }
}
