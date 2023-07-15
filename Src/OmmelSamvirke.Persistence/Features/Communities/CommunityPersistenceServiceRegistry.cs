using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Persistence.Features.Communities.Repositories;

namespace OmmelSamvirke.Persistence.Features.Communities;

public static class CommunityPersistenceServiceRegistry {
    public static void AddCommunitiesPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(ICommunityRepository), typeof(CommunityRepository));
    }
}
