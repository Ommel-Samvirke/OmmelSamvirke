using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class PageRepository : GenericRepository<Page>, IPageRepository
{
    private readonly AppDbContext _dbContext;

    public PageRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Page>> GetByPageTemplateId(int pageTemplateId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Pages.AsNoTracking()
            .Where(p => p.TemplateId == pageTemplateId)
            .ToListAsync(cancellationToken);
    }
}
