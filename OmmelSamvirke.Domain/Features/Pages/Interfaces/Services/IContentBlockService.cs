using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Services;

public interface IContentBlockService
{
    Task<T> CreateContentBlock<T>(bool isOptional, int xPosition, int yPosition, int width, int? height) where T: ContentBlock;
    Task<T> UpdateContentBlock<T>(int id, bool isOptional, int xPosition, int yPosition, int width, int? height) where T: ContentBlock;
    Task<bool> DeleteContentBlock(int id);
}
