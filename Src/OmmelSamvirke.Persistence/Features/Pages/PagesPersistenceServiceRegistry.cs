using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Persistence.Features.Common.Repositories;
using OmmelSamvirke.Persistence.Features.Pages.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages;

public static class PagesPersistenceServiceRegistry
{
    public static void AddPagesPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(
            typeof(IContentBlockRepository),
            typeof(ContentBlockRepository)
        );
        services.AddScoped(
            typeof(IPageRepository),
            typeof(PageRepository)
        );
        services.AddScoped(
            typeof(IPageTemplateRepository),
            typeof(PageTemplateRepository)
        );
        services.AddScoped(
            typeof(IContentBlockLayoutConfigurationRepository),
            typeof(ContentBlockLayoutConfigurationRepository)
        );

        services.AddScoped(
            typeof(IContentBlockDataRepository<HeadlineBlockData, HeadlineBlock>),
            typeof(ContentBlockDataRepository<HeadlineBlockData, HeadlineBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockDataRepository<ImageBlockData, ImageBlock>),
            typeof(ContentBlockDataRepository<ImageBlockData, ImageBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockDataRepository<PdfBlockData, PdfBlock>),
            typeof(ContentBlockDataRepository<PdfBlockData, PdfBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockDataRepository<SlideshowBlockData, SlideshowBlock>),
            typeof(ContentBlockDataRepository<SlideshowBlockData, SlideshowBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockDataRepository<TextBlockData, TextBlock>),
            typeof(ContentBlockDataRepository<TextBlockData, TextBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockDataRepository<VideoBlockData, VideoBlock>),
            typeof(ContentBlockDataRepository<VideoBlockData, VideoBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockDataRepositoriesAggregate),
            typeof(ContentBlockDataRepositoriesAggregate)
        );
    }
}
