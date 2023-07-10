using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Services;

public interface ISlideshowBlockDataService
{
    Task<SlideshowBlockData> CreateSlideshowBlockData(SlideshowBlock slideshowBlock, List<string> imageUrls, int pageId);
    Task<SlideshowBlockData> UpdateSlideshowBlockData(int id, SlideshowBlock slideshowBlock, List<string> imageUrls, int pageId);
    Task<bool> DeleteSlideshowBlockData(int id);
}
