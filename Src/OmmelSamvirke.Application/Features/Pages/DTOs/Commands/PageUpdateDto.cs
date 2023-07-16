namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class PageUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int PageTemplateId { get; set; }
}
