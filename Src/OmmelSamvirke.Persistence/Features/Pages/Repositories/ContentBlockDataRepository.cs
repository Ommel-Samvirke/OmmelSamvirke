using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class ContentBlockDataRepository : IContentBlockDataRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<IContentBlockData> _dbSet;

    public ContentBlockDataRepository(AppDbContext dbContext)
    {
        _context = dbContext;
        _dbSet = dbContext.Set<IContentBlockData>();
    }
    
    public async Task<List<IContentBlockData>> GetByPageIdAsync(int pageId, CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking()
            .Where(e => e.Page != null && e.Page.Id == pageId)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<IContentBlockData>> CreateAsync(List<IContentBlockData> contentBlockData, CancellationToken cancellationToken = default)
    {
        await _dbSet.AddRangeAsync(contentBlockData, cancellationToken);
        foreach (IContentBlockData blockData in contentBlockData)
            _context.Entry(blockData).State = EntityState.Added;

        await _context.SaveChangesAsync(cancellationToken);
        return contentBlockData;
    }

    public async Task<List<IContentBlockData>> UpdateAsync(List<IContentBlockData> contentBlockData, CancellationToken cancellationToken = default)
    {
        _dbSet.UpdateRange(contentBlockData);
        
        foreach (IContentBlockData entity in contentBlockData) 
            _dbSet.Entry(entity).State = EntityState.Modified;
        
        await _context.SaveChangesAsync(cancellationToken);
        return contentBlockData;
    }

    public async Task<bool> DeleteAsync(List<IContentBlockData> contentBlockData, CancellationToken cancellationToken = default)
    {
        try
        {
            _dbSet.RemoveRange(contentBlockData);
            foreach (IContentBlockData entity in contentBlockData) 
                _dbSet.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception )
        {
            return false;
        }
    }
}
