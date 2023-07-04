using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;

public class PageTemplateDto
{
    public int PageTemplateId { get; }
    public string Name { get; }
    public List<ContentBlock> ContentBlocks { get; }
    public PageTemplateState PageTemplateState { get; }

    public PageTemplateDto(
        int pageTemplateId,
        string name,
        List<ContentBlock> contentBlocks,
        PageTemplateState pageTemplateState
    )
    {
        PageTemplateId = pageTemplateId;
        Name = name;
        ContentBlocks = contentBlocks;
        PageTemplateState = pageTemplateState;
    }
}
