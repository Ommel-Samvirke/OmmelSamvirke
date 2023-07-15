using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class PageTemplateCreateDto
{
    public string Name { get; set;  }
    public PageTemplateState State { get; set; }

    public PageTemplateCreateDto(string name)
    {
        Name = name;
        State = PageTemplateState.Hidden;
    }
    
    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public PageTemplateCreateDto()
    {
        
    }
}
