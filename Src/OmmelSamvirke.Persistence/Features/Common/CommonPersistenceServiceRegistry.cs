using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Common;

public static class CommonPersistenceServiceRegistry
{
    public static void AddCommonPersistenceServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
