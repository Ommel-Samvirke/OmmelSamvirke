namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class PageUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int PageTemplateId { get; set; }

    public PageUpdateDto(int id, string name, int templateId)
    {
        Id = id;
        Name = name;
        PageTemplateId = templateId;
    }

    /// <summary>
    /// Needed for deserialization
    /// </summary>
    public PageUpdateDto()
    {
        
    }
}
