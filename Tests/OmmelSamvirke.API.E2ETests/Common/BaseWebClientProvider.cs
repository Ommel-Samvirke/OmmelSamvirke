using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Common;

public abstract class BaseWebClientProvider
{
    protected HttpClient Client = null!;
    internal WebApplicationFactory<Program> Factory = null!;

    [SetUp]
    public virtual void SetUp()
    {
        Factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing DbContext configuration
                    ServiceDescriptor? descriptor = services.SingleOrDefault(
                        d => d.ServiceType ==
                             typeof(DbContextOptions<AppDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddSingleton<ITestDbProvider, TestDbProvider>();
                    
                    services.AddSingleton<DbContextOptions<AppDbContext>>(provider =>
                    {
                        ITestDbProvider dbProvider = provider.GetRequiredService<ITestDbProvider>();
                        return dbProvider.DbContextOptions;
                    });

                    services.AddTransient<AppDbContext>(provider =>
                    {
                        ITestDbProvider dbProvider = provider.GetRequiredService<ITestDbProvider>();
                        return dbProvider.CreateDbContext();
                    });
                });
            });

        Client = Factory.CreateClient();
    }
    
    [TearDown]
    public virtual void TearDown()
    {
        Client.Dispose();
        Factory.Dispose();
    }
}
