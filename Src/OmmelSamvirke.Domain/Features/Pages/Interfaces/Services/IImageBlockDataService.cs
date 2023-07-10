using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Services;

public interface IImageBlockDataService
{
    Task<ImageBlockData> CreateImageBlockData(ImageBlock imageBlock, string imageUrl, int pageId);
    Task<ImageBlockData> UpdateImageBlockData(int id, ImageBlock imageBlock, string imageUrl, int pageId);
    Task<bool> DeleteImageBlockData(int id);
}
