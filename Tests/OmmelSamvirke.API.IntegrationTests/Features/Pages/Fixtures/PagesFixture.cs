using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;

public class PagesFixture
{
    private readonly AppDbContext _dbContext;

    public PagesFixture(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        PopulateDatabase();
    }

    private void PopulateDatabase()
    {
        ClearTables();
        PopulatePageTemplates();
    }
    
    private void PopulatePageTemplates()
    {
        PageTemplate pageTemplate = GlobalPagesFixtures.DefaultPageTemplate();
        
        _dbContext.PageTemplates.Add(pageTemplate);
        _dbContext.SaveChanges();
    }
    
    private void ClearTables()
    {
        _dbContext.PageTemplates.RemoveRange(_dbContext.PageTemplates);
        _dbContext.SaveChanges();
    }
}
