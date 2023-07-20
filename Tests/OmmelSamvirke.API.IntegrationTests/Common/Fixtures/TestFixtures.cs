using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Common.Fixtures;

public partial class TestFixtures
{
    private readonly AppDbContext _dbContext;

    public TestFixtures(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        ClearTables();
    }

    private void ClearTables()
    {
        _dbContext.PageTemplates.RemoveRange(_dbContext.PageTemplates);
        _dbContext.Pages.RemoveRange(_dbContext.Pages);
        _dbContext.Communities.RemoveRange(_dbContext.Communities);
        _dbContext.HeadlineBlockData.RemoveRange(_dbContext.HeadlineBlockData);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
    }
}
