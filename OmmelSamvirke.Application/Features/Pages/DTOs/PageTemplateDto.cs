using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;

public class PageTemplateDto
{
    public int Id { get; set;  }
    public string Name { get; set;  }
    public List<ContentBlockDto> ContentBlocks { get; set; }
    public PageTemplateState PageTemplateState { get; set;  }

    public PageTemplateDto(
        int id,
        string name,
        List<ContentBlockDto> contentBlocks,
        PageTemplateState pageTemplateState
    )
    {
        Id = id;
        Name = name;
        ContentBlocks = contentBlocks;
        PageTemplateState = pageTemplateState;
    }
}
