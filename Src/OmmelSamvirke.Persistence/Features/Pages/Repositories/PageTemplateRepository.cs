using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class PageTemplateRepository : IPageTemplateRepository
{
    private readonly GenericRepository<PageTemplate> _genericRepository;
    public PageTemplateRepository(AppDbContext dbContext)
    {
        _genericRepository = new GenericRepository<PageTemplate>(dbContext);
    }
    
    public async Task<IReadOnlyList<PageTemplate>> GetAsync()
    {
        return await _genericRepository.GetAsync();
    }

    public async Task<PageTemplate?> GetByIdAsync(int id)
    {
        return await _genericRepository.GetByIdAsync(id);
    }

    public async Task<PageTemplate> CreateAsync(PageTemplate entity)
    {
        return await _genericRepository.CreateAsync(entity);
    }

    public async Task<PageTemplate> UpdateAsync(PageTemplate entity)
    {
        return await _genericRepository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(PageTemplate entity)
    {
        return await _genericRepository.DeleteAsync(entity);
    }
}
