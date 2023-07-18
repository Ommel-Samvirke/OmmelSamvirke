using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Communities.Models;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using Swashbuckle.AspNetCore.Filters;

namespace OmmelSamvirke.API.Controllers.Features.Pages.Examples;

public class CreatePageFromTemplateCommandExample : IExamplesProvider<CreatePageFromTemplateCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CreatePageFromTemplateCommandExample(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public CreatePageFromTemplateCommand GetExamples()
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        IPageTemplateRepository pageTemplateRepository = scope.ServiceProvider.GetRequiredService<IPageTemplateRepository>();
        ICommunityRepository communityRepository = scope.ServiceProvider.GetRequiredService<ICommunityRepository>();

        PageTemplate? firstPage = Task.Run(() => pageTemplateRepository.GetAsync()).Result.FirstOrDefault();
        Community? firstCommunity = Task.Run(() => communityRepository.GetAsync()).Result.FirstOrDefault();

        if (firstPage is null || firstCommunity is null)
            return null!;
        
        return new CreatePageFromTemplateCommand
        {
            PageName = "Example Page",
            PageTemplateId = firstPage.Id,
            CommunityId = firstCommunity.Id
        };
    }
}
