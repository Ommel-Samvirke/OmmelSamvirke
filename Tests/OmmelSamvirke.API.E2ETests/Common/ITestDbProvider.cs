using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Common;

public interface ITestDbProvider
{
    public DbContextOptions<AppDbContext> DbContextOptions { get; }
    public AppDbContext CreateDbContext();
}
