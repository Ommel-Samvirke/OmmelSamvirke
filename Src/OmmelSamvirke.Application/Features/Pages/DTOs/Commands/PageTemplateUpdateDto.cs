using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class PageTemplateUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set;  }
    public List<ContentBlockCreateDto> ContentBlocks { get; set; }
    public PageTemplateState PageTemplateState { get; set;  }

    public PageTemplateUpdateDto(
        int id,
        string name,
        List<ContentBlockCreateDto> contentBlocks,
        PageTemplateState pageTemplateState
    )
    {
        Id = id;
        Name = name;
        ContentBlocks = contentBlocks;
        PageTemplateState = pageTemplateState;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public PageTemplateUpdateDto()
    {
        
    }
}
