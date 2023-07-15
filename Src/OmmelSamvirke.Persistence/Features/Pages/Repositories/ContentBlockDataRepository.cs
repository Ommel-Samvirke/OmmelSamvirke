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
    
    public async Task<List<IContentBlockData>> GetByPageIdAsync(int pageId)
    {
        return await _dbSet.AsNoTracking().Where(e => e.Page.Id == pageId).ToListAsync();
    }

    public async Task<List<IContentBlockData>> CreateAsync(List<IContentBlockData> contentBlockData)
    {
        await _dbSet.AddRangeAsync(contentBlockData);
        await _context.SaveChangesAsync();
        return contentBlockData;
    }

    public async Task<List<IContentBlockData>> UpdateAsync(List<IContentBlockData> contentBlockData)
    {
        _dbSet.UpdateRange(contentBlockData);
        
        foreach (IContentBlockData entity in contentBlockData) 
            _dbSet.Entry(entity).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();
        return contentBlockData;
    }

    public async Task<bool> DeleteAsync(List<IContentBlockData> contentBlockData)
    {
        try
        {
            _dbSet.RemoveRange(contentBlockData);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception )
        {
            return false;
        }
    }
}
