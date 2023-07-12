namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IContentBlockDataRepository
{
    Task<List<IContentBlockData>> GetByPageIdAsync(int pageId);
    Task<List<IContentBlockData>> CreateAsync(List<IContentBlockData> contentBlockData);
    Task<List<IContentBlockData>> UpdateAsync(List<IContentBlockData> contentBlockData);
}
