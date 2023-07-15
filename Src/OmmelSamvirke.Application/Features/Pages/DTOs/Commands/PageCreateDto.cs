namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class PageCreateDto
{
    public string Name { get; set; }
    public int PageTemplateId { get; set; }

    public PageCreateDto(string name, int templateId)
    {
        Name = name;
        PageTemplateId = templateId;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public PageCreateDto()
    {
        
    }
}
