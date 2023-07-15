using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class PageRepository : IPageRepository
{
    private readonly GenericRepository<Page> _genericRepository;
    private readonly DbSet<Page> _dbSet;
    
    public PageRepository(AppDbContext dbContext)
    {
        _dbSet = dbContext.Set<Page>();
        _genericRepository = new GenericRepository<Page>(dbContext);
    }

    public async Task<IReadOnlyList<Page>> GetAsync()
    {
        return await _genericRepository.GetAsync();
    }

    public async Task<Page?> GetByIdAsync(int id)
    {
        return await _genericRepository.GetByIdAsync(id);
    }

    public async Task<Page> CreateAsync(Page entity)
    {
        return await _genericRepository.CreateAsync(entity);
    }

    public async Task<Page> UpdateAsync(Page entity)
    {
        return await _genericRepository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(Page entity)
    {
        return await _genericRepository.DeleteAsync(entity);
    }

    public async Task<List<Page>> GetByPageTemplateId(int pageTemplateId)
    {
        return await _dbSet.AsNoTracking().Where(p => p.Template.Id == pageTemplateId).ToListAsync();
    }
}
