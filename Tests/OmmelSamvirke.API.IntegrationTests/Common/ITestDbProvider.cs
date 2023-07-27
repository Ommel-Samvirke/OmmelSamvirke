using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.IntegrationTests.Common;

public interface ITestDbProvider
{
    public DbContextOptions<AppDbContext> DbContextOptions { get; }
    public AppDbContext CreateDbContext();
}
