using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Services;

public interface IVideoBlockDataService
{
    Task<VideoBlockData> CreateVideoBlockData(VideoBlock videoBlock, string videoUrl, int pageId);
    Task<VideoBlockData> UpdateVideoBlockData(int id, VideoBlock videoBlock, string videoUrl, int pageId);
    Task<bool> DeleteVideoBlockData(int id);
}
