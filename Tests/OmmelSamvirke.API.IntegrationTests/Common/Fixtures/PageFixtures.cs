using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Common.Fixtures;

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
}
