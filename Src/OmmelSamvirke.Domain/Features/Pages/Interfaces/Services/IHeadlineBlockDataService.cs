using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Services;

public interface IHeadlineBlockDataService
{
    Task<HeadlineBlockData> CreateHeadlineBlockData(HeadlineBlock headlineBlock, string headline, int pageId);
    Task<HeadlineBlockData> UpdateHeadlineBlockData(int id, HeadlineBlock headlineBlock, string headline, int pageId);
    Task<bool> DeleteHeadlineBlockData(int id);
}
