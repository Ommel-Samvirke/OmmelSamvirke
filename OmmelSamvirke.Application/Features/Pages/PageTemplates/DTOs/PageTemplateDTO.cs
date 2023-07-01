using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;

public class PageTemplateDto
{
    public int PageTemplateId { get; }
    public string Name { get; }
    public ISet<Layouts> SupportedLayouts { get; }
    public List<ContentBlock> ContentBlocks { get; }
    public PageTemplateState PageTemplateState { get; }

    public PageTemplateDto(
        int pageTemplateId,
        string name,
        ISet<Layouts> supportedLayouts,
        List<ContentBlock> contentBlocks,
        PageTemplateState pageTemplateState
    )
    {
        PageTemplateId = pageTemplateId;
        Name = name;
        SupportedLayouts = supportedLayouts;
        ContentBlocks = contentBlocks;
        PageTemplateState = pageTemplateState;
    }
}
