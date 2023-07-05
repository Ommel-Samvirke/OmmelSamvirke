namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;

public class PageDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public PageTemplateDto Template { get; set; }
    
    
    public PageDto(int id, string name, PageTemplateDto template)
    {
        Id = id;
        Name = name;
        Template = template;
    }
}
