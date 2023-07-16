using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Persistence.Features.Pages.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages;

public static class PagesPersistenceServiceRegistry
{
    public static void AddPagesPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IContentBlockDataRepository), typeof(ContentBlockDataRepository));
        services.AddScoped(typeof(IContentBlockRepository), typeof(ContentBlockRepository));
        services.AddScoped(typeof(IPageRepository), typeof(PageRepository));
        services.AddScoped(typeof(IPageTemplateRepository), typeof(PageTemplateRepository));
        services.AddScoped(typeof(IContentBlockLayoutConfigurationRepository), typeof(ContentBlockLayoutConfigurationRepository));
    }
}
