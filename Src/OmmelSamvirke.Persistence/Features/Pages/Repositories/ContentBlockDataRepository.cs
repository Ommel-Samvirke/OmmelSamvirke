using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class ContentBlockDataRepository<T, TU> : IContentBlockDataRepository<T, TU> where TU : ContentBlock where T : ContentBlockData<TU> 
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public ContentBlockDataRepository(AppDbContext dbContext)
    {
        _context = dbContext;
        _dbSet = dbContext.Set<T>();
    }
    
    public async Task<List<IContentBlockData>> GetByPageIdAsync(int pageId, CancellationToken cancellationToken = default)
    {
        List<T> result = await _dbSet.AsNoTracking()
            .Include(e => e.Page)
            .Include(e => e.ContentBlock)
            .Where(e => e.PageId == pageId)
            .ToListAsync(cancellationToken);
            
        return result.Cast<IContentBlockData>().ToList();
    }

    public async Task<List<IContentBlockData>> CreateAsync(List<IContentBlockData> entities, CancellationToken cancellationToken = default)
    {
        List<T> contentBlockData = entities.Cast<T>().ToList();

        await _dbSet.AddRangeAsync(contentBlockData, cancellationToken);
        foreach (IContentBlockData blockData in contentBlockData)
            _context.Entry(blockData).State = EntityState.Added;

        await _context.SaveChangesAsync(cancellationToken);
        return contentBlockData.Cast<IContentBlockData>().ToList();
    }
    
    public async Task<List<IContentBlockData>> UpdateAsync(List<IContentBlockData> entities, CancellationToken cancellationToken = default)
    {
        List<T> contentBlockData = entities.Cast<T>().ToList();

        _dbSet.UpdateRange(contentBlockData);
        
        foreach (T entity in contentBlockData) 
            _dbSet.Entry(entity).State = EntityState.Modified;
        
        await _context.SaveChangesAsync(cancellationToken);
        return contentBlockData.Cast<IContentBlockData>().ToList();
    }

    public async Task<bool> DeleteAsync(List<IContentBlockData> entities, CancellationToken cancellationToken = default)
    {
        try
        {
            List<T> contentBlockData = entities.Cast<T>().ToList();

            _dbSet.RemoveRange(contentBlockData);
            foreach (T entity in contentBlockData) 
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
