using OmmelSamvirke.Domain.Features.Pages.Interfaces.ContentBlockData;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IContentBlockDataRepository
{
    Task<List<IContentBlockData>> GetByPageIdAsync(int pageId);
}
