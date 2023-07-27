using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.TestUtilities.Features.Pages;
using Swashbuckle.AspNetCore.Filters;

namespace OmmelSamvirke.API.Controllers.Features.Pages.Examples;

public class CreatePageExample : IExamplesProvider<CreatePageCommand>
{
    public CreatePageCommand GetExamples()
    {
        return new CreatePageCommand
        {
            Page = GlobalPageFixtures.DefaultPageDto(),
        };
    }
}
