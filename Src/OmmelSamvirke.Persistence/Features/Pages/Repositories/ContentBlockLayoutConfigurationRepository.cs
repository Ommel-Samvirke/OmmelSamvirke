using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class ContentBlockLayoutConfigurationRepository : GenericRepository<ContentBlockLayoutConfiguration>, IContentBlockLayoutConfigurationRepository
{
    public ContentBlockLayoutConfigurationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> DeleteAsync(List<ContentBlockLayoutConfiguration?> contentBlocksLayoutConfigurations,
        CancellationToken cancellationToken = default)
    {
        List<ContentBlockLayoutConfiguration> toBeDeleted = contentBlocksLayoutConfigurations
            .Where(c => c is not null)
            .ToList()!;

        DbSet.RemoveRange(toBeDeleted);
            foreach (ContentBlockLayoutConfiguration entity in toBeDeleted) 
                DbSet.Entry(entity).State = EntityState.Deleted;
            
            await DbDbContext.SaveChangesAsync(cancellationToken);
            return true;
    }
}
