using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Admins;
using OmmelSamvirke.Persistence.Features.Common;
using OmmelSamvirke.Persistence.Features.Communities;
using OmmelSamvirke.Persistence.Features.Pages;

namespace OmmelSamvirke.Persistence;

public static class PersistenceServiceRegistry
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultDbConnectionString"));
        });
        
        services.AddCommonPersistenceServices();
        services.AddCommunitiesPersistenceServices(configuration);
        services.AddAdminsPersistenceServices(configuration);
        services.AddPagesPersistenceServices(configuration);

        return services;
    }
}
