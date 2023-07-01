using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace OmmelSamvirke.Application;

public static class ApplicationServiceRegistry
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(_ => {
            // No config yet
        }, Assembly.GetExecutingAssembly());

        return services;
    }
}