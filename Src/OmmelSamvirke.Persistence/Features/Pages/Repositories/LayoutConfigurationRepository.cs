using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class LayoutConfigurationRepository : GenericRepository<LayoutConfiguration>, ILayoutConfigurationRepository
{
    private readonly DbSet<LayoutConfiguration> _layoutConfigurationsSet;

    public LayoutConfigurationRepository(AppDbContext dbContext) : base(dbContext)
    {
        _layoutConfigurationsSet = dbContext.Set<LayoutConfiguration>();
    }

    public async Task<bool> DeleteByPageAsync(Page page, CancellationToken cancellationToken = default)
    {
        List<LayoutConfiguration> toBeDeleted = new()
        {
            page.DesktopConfiguration,
            page.TabletConfiguration,
            page.MobileConfiguration
        };

        _layoutConfigurationsSet.RemoveRange(toBeDeleted);
        await DbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public override async Task<IReadOnlyList<LayoutConfiguration>> GetWithRelationsAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.ContentBlocks)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public override async Task<LayoutConfiguration?> GetByIdWithRelationsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.ContentBlocks)
            .Where(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
}
