using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;

public class PageTemplateDto
{
    public int Id { get; set;  }
    public string Name { get; set;  }
    public List<ContentBlockDto> ContentBlocks { get; set; }
    public PageTemplateState PageTemplateState { get; set;  }

    public PageTemplateDto()
    {
        ContentBlocks = new List<ContentBlockDto>();
    }
    
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
