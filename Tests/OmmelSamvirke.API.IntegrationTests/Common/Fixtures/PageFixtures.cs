using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.IntegrationTests.Common.Fixtures;

public partial class TestFixtures
{
    public Page InsertPage()
    {
        Page page = GlobalPageFixtures.DefaultPage();
        _dbContext.Pages.Add(page);
        
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();

        return page;
    }

    public Page InsertPage(Page page)
    {
        _dbContext.Pages.Add(page);
        
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();

        return page;
    }

    public int CountPages()
    {
        return _dbContext.Pages.Count();
    }
}
