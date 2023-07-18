using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IContentBlockLayoutConfigurationRepository : IGenericRepository<ContentBlockLayoutConfiguration>
{
    Task<bool> DeleteAsync(List<ContentBlockLayoutConfiguration?> contentBlocks,
        CancellationToken cancellationToken = default);
}
