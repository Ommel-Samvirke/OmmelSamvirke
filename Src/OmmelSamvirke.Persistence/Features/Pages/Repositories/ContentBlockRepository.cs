using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class ContentBlockRepository<T> : IContentBlockRepository<T> where T : ContentBlock 
{
    private readonly DbSet<LayoutConfiguration> _layoutConfigurations;

    public ContentBlockRepository(AppDbContext dbContext)
    {
        _layoutConfigurations = dbContext.LayoutConfigurations;
    }

    public async Task<List<T>> GetByLayoutConfiguration(int layoutConfigurationId, CancellationToken cancellationToken)
    {
        LayoutConfiguration? layoutConfig = await _layoutConfigurations
            .Include(p => p.ContentBlocks)
            .FirstOrDefaultAsync(x =>
                x.Id == layoutConfigurationId, 
                cancellationToken: cancellationToken
            );
        
        if (layoutConfig is null)
            return new List<T>();

        if (typeof(T) == typeof(ContentBlock))
        {
            return layoutConfig.ContentBlocks
                .Cast<T>()
                .ToList();
        }
        
        return layoutConfig.ContentBlocks
            .OfType<T>()
            .ToList();
    }
}
