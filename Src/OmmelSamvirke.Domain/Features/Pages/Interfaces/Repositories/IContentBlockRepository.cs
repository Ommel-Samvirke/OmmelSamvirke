using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IContentBlockRepository<T> where T : ContentBlock
{
    public Task<List<T>> GetByLayoutConfiguration(int layoutConfigurationId, CancellationToken cancellationToken = default);
}
