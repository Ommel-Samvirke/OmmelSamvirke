using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces;

public interface IContentBlockData
{
    ContentBlock? BaseContentBlock { get; }
    Page? Page { get; }
}
