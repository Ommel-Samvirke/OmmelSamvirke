using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.ContentBlockData;

public interface IContentBlockData
{
    ContentBlock BaseContentBlock { get; }
    int PageId { get; }
}
