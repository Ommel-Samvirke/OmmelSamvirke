using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs;

public class PageTemplateWithoutContentBlocksDto
{
    public int Id { get; set;  }
    public string Name { get; set;  }
    public PageTemplateState PageTemplateState { get; set;  }

    public PageTemplateWithoutContentBlocksDto(
        int id,
        string name,
        PageTemplateState pageTemplateState
    )
    {
        Id = id;
        Name = name;
        PageTemplateState = pageTemplateState;
    }
}
