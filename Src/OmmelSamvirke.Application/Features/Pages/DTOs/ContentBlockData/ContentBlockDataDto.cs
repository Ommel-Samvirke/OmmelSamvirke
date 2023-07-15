using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;

public abstract class ContentBlockDataDto
{
    protected int Id { get; set; }
    protected ContentBlockDto ContentBlock { get; set; }
    protected PageDto Page { get; set; }

    protected ContentBlockDataDto(int id, ContentBlockDto contentBlock, PageDto page)
    {
        Id = id;
        ContentBlock = contentBlock;
        Page = page;
    }
}
