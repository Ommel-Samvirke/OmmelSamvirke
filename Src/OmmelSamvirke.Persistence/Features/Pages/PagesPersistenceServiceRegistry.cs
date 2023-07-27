using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Persistence.Features.Pages.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages;

public static class PagesPersistenceServiceRegistry
{
    public static void AddPagesPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(
            typeof(IContentBlockRepository<ContentBlock>),
            typeof(ContentBlockRepository<ContentBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockRepository<HeadlineBlock>),
            typeof(ContentBlockRepository<HeadlineBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockRepository<ImageBlock>),
            typeof(ContentBlockRepository<ImageBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockRepository<PdfBlock>),
            typeof(ContentBlockRepository<PdfBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockRepository<SlideshowBlock>),
            typeof(ContentBlockRepository<SlideshowBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockRepository<TextBlock>),
            typeof(ContentBlockRepository<TextBlock>)
        );
        services.AddScoped(
            typeof(IContentBlockRepository<VideoBlock>),
            typeof(ContentBlockRepository<VideoBlock>)
        );
        services.AddScoped(
            typeof(IPageRepository),
            typeof(PageRepository)
        );
        services.AddScoped(
            typeof(ILayoutConfigurationRepository),
            typeof(LayoutConfigurationRepository)
        );
    }
}
