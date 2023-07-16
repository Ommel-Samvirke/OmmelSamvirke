using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class PageTemplateRepository : GenericRepository<PageTemplate>, IPageTemplateRepository
{
    private readonly AppDbContext _dbContext;

    public PageTemplateRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<PageTemplate?> GetByIdAsyncWithNavigationProps(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.PageTemplates
            .Include(x => x.ContentBlocks)
            .ThenInclude(x => x.DesktopConfiguration)
            .Include(x => x.ContentBlocks)
            .ThenInclude(x => x.TabletConfiguration)
            .Include(x => x.ContentBlocks)
            .ThenInclude(x => x.MobileConfiguration)
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
}
