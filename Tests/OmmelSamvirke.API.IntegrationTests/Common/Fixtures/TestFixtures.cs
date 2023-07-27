using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Common.Fixtures;

public partial class TestFixtures
{
    private readonly AppDbContext _dbContext;

    public TestFixtures(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}
