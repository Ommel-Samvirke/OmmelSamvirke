namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IContentBlockDataRepository
{
    Task<List<IContentBlockData>> GetByPageIdAsync(int pageId, CancellationToken cancellationToken = default);
    Task<List<IContentBlockData>> CreateAsync(List<IContentBlockData> contentBlockData, CancellationToken cancellationToken = default);
    Task<List<IContentBlockData>> UpdateAsync(List<IContentBlockData> contentBlockData, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(List<IContentBlockData> contentBlockData, CancellationToken cancellationToken = default);
}
