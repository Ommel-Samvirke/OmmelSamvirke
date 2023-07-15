using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace OmmelSamvirke.API.Controllers.Features.Pages.Examples;

public class CreatePageTemplateCommandExample : IExamplesProvider<CreatePageTemplateCommand>
{
    public CreatePageTemplateCommand  GetExamples()
    {
        return new CreatePageTemplateCommand
        {
            PageTemplateCreateDto = new PageTemplateCreateDto
            {
                Name = "Example Page Template",
                State = PageTemplateState.Hidden
            }
        };
    }
}
