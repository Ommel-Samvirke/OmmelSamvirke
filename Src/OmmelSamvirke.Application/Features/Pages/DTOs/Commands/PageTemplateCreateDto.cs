using OmmelSamvirke.Domain.Features.Pages.Enums;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Commands;

public class PageTemplateCreateDto
{
    public string Name { get; set; } = string.Empty;
    public PageTemplateState State { get; set; }
}
