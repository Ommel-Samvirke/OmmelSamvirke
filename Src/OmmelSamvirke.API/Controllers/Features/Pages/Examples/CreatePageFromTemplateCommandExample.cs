using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using Swashbuckle.AspNetCore.Filters;

namespace OmmelSamvirke.API.Controllers.Features.Pages.Examples;

public class CreatePageFromTemplateCommandExample : IExamplesProvider<CreatePageFromTemplateCommand>
{
    public CreatePageFromTemplateCommand GetExamples()
    {
        return new CreatePageFromTemplateCommand
        {
            PageName = "Example Page",
            PageTemplateId = 1,
            CommunityId = 1
        };
    }
}
