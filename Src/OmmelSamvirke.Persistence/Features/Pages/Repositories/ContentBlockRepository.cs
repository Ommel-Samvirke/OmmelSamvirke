using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class ContentBlockRepository : GenericRepository<ContentBlock>, IContentBlockRepository
{
    private readonly AppDbContext _dbContext;

    public ContentBlockRepository(AppDbContext dbDbContext) : base(dbDbContext)
    {
        _dbContext = dbDbContext;
    }

    public async Task<List<ContentBlock>> CreateAsync(List<ContentBlock> contentBlocks, CancellationToken cancellationToken = default)
    {
        await DbSet.AddRangeAsync(contentBlocks, cancellationToken);
        foreach (ContentBlock entity in contentBlocks) 
            DbSet.Entry(entity).State = EntityState.Added;
        
        await DbDbContext.SaveChangesAsync(cancellationToken);
        return contentBlocks;
    }

    public async Task<bool> DeleteAsync(List<ContentBlock> contentBlocks, CancellationToken cancellationToken = default)
    {
        try
        {
            DbSet.RemoveRange(contentBlocks);
            foreach (ContentBlock entity in contentBlocks) 
                DbSet.Entry(entity).State = EntityState.Deleted;
            
            await DbDbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception )
        {
            return false;
        }
    }

    public async Task<List<ContentBlock>> GetByPageTemplateIdAsync(int pageTemplateId, CancellationToken cancellationToken = default)
    {
        IQueryable<int> contentBlockIds = _dbContext.PageTemplates.AsNoTracking().Where(pageTemplate => pageTemplate.Id == pageTemplateId)
            .SelectMany(pageTemplate => pageTemplate.ContentBlocks.Select(contentBlock => contentBlock.Id))
            .Distinct();

        if (!contentBlockIds.Any())
            return new List<ContentBlock>();
        
        return await _dbContext.ContentBlocks.AsNoTracking().Where(contentBlock => contentBlockIds.Contains(contentBlock.Id))
            .Include(cb => cb.DesktopConfiguration).AsNoTracking()
            .Include(cb => cb.TabletConfiguration).AsNoTracking()
            .Include(cb => cb.MobileConfiguration).AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}
