using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IContentBlockDataRepository<T, TU> where TU : ContentBlock where T : ContentBlockData<TU>
{
    Task<List<IContentBlockData>> GetByPageIdAsync(int pageId, CancellationToken cancellationToken = default);

    Task<List<IContentBlockData>> CreateAsync(List<IContentBlockData> entities, CancellationToken cancellationToken = default);
    Task<List<IContentBlockData>> UpdateAsync(List<IContentBlockData> entities, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(List<IContentBlockData> entities, CancellationToken cancellationToken = default);
}
