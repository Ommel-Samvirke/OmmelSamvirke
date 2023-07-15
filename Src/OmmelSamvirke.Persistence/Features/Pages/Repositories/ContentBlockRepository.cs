using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class ContentBlockRepository : GenericRepository<ContentBlock>, IContentBlockRepository
{
    private readonly AppDbContext _context;
    private new readonly DbSet<ContentBlock> _dbSet;

    public ContentBlockRepository(AppDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
        _dbSet = dbContext.Set<ContentBlock>();
    }

    public async Task<List<ContentBlock>> CreateAsync(List<ContentBlock> contentBlocks)
    {
        await _dbSet.AddRangeAsync(contentBlocks);
        await _context.SaveChangesAsync();
        return contentBlocks;
    }

    public async Task<bool> DeleteAsync(List<ContentBlock> contentBlocks)
    {
        try
        {
            _dbSet.RemoveRange(contentBlocks);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception )
        {
            return false;
        }
    }
}
