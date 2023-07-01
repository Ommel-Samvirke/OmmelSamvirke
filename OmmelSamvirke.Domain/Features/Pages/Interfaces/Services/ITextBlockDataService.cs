using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Services;

public interface ITextBlockDataService
{
    Task<TextBlockData> CreateTextBlockData(TextBlock textBlock, string text, int pageId);
    Task<TextBlockData> UpdateTextBlockData(int id, TextBlock textBlock, string text, int pageId);
    Task<bool> DeleteTextBlockData(int id);
}
