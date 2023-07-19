using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;

public class PagesFixture
{
    private readonly AppDbContext _dbContext;

    public PagesFixture(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        PopulateDatabase();
    }

    public void PopulateDatabase()
    {
        PopulatePageTemplates();
    }
    
    private void PopulatePageTemplates()
    {
        PageTemplate pageTemplate = CreateEntity(new PageTemplate
        {
            Name = "TestTemplateInMemory",
            State = PageTemplateState.Public
        });
        
        _dbContext.PageTemplates.Add(pageTemplate);
        _dbContext.SaveChanges();
    }
    
    private static T CreateEntity<T>(T entity) where T : BaseModel
    {
        entity.DateCreated = DateTime.UtcNow;
        entity.DateModified = entity.DateCreated;
        return entity;
    }
}
