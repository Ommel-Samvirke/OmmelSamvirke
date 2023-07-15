using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.Domain.Features.Admins.Interfaces.Repositories;
using OmmelSamvirke.Persistence.Features.Admins.Repositories;

namespace OmmelSamvirke.Persistence.Features.Admins;

public static class AdminsPersistenceServiceRegistry
{
    public static void AddAdminsPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(typeof(IAdminRepository), typeof(AdminRepository));
    }
}
