using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Common;

public class TestDbProvider : ITestDbProvider
{
    public DbContextOptions<AppDbContext> DbContextOptions { get; }

    public TestDbProvider()
    {
        ServiceProvider serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        DbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .UseInternalServiceProvider(serviceProvider)
            .Options;
    }

    public AppDbContext CreateDbContext()
    {
        return new AppDbContext(DbContextOptions);
    }
}
