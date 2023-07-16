using AutoMapper;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using Swashbuckle.AspNetCore.Filters;

namespace OmmelSamvirke.API.Controllers.Features.Pages.Examples;

public class UpdatePageTemplateCommandExample : IExamplesProvider<UpdatePageTemplateCommand>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private const int PageTemplateId = 1;

    public UpdatePageTemplateCommandExample(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }
    
    public UpdatePageTemplateCommand GetExamples()
    {
        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        
        IMapper mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
        IPageTemplateRepository pageTemplateRepository = scope.ServiceProvider.GetRequiredService<IPageTemplateRepository>();
        
        Random random = new();
        PageTemplate? pageTemplate = Task.Run(() => pageTemplateRepository.GetByIdAsyncWithNavigationProps(PageTemplateId)).Result;
        if (pageTemplate is null)
            return null!;

        PageTemplateQueryDto originalPageTemplate = mapper.Map<PageTemplateQueryDto>(pageTemplate);
        PageTemplateUpdateDto updatedPageTemplate = new()
        {
            Id = PageTemplateId,
            Name = $"Updated Page Template {random.NextInt64(100)}",
            State = PageTemplateState.Public,
            ContentBlocks = new List<ContentBlockCreateDto>
            {
                new(
                    false,
                    new ContentBlockLayoutConfigurationCreateDto(
                        (int)random.NextInt64(0, 9),
                        (int)random.NextInt64(0, 9),
                        (int)random.NextInt64(1, 10),
                        (int)random.NextInt64(1, 10)
                    ),
                    new ContentBlockLayoutConfigurationCreateDto(
                        (int)random.NextInt64(0, 9),
                        (int)random.NextInt64(0, 9),
                        (int)random.NextInt64(1, 10),
                        (int)random.NextInt64(1, 10)    
                    ),
                    new ContentBlockLayoutConfigurationCreateDto(
                        (int)random.NextInt64(0, 9),
                        (int)random.NextInt64(0, 9),
                        (int)random.NextInt64(1, 10),
                        (int)random.NextInt64(1, 10)
                    ),
                    ContentBlockType.HeadlineBlock
                )
            }
        };

        return new UpdatePageTemplateCommand(originalPageTemplate, updatedPageTemplate);
    }
}
