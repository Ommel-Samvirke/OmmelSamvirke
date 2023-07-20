using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Common.Fixtures;

public partial class TestFixtures
{
    public PageTemplate InsertPageTemplate(PageTemplateState state = PageTemplateState.Public)
    {
        PageTemplate pageTemplate = GlobalPageTemplatesFixtures.DefaultPageTemplate();
        pageTemplate.State = state;
        
        _dbContext.PageTemplates.Add(pageTemplate);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();

        return pageTemplate;
    }
    
    public PageTemplate InsertPageTemplate(PageTemplate pageTemplate)
    {
        _dbContext.PageTemplates.Add(pageTemplate);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();

        return pageTemplate;
    }

    public List<PageTemplate> InsertPageTemplates(List<PageTemplateState> states)
    {
        List<PageTemplate> pageTemplates = new();
        
        foreach (PageTemplateState pageTemplateState in states)
        {
            PageTemplate pageTemplate = GlobalPageTemplatesFixtures.DefaultPageTemplate();
            pageTemplate.State = pageTemplateState;
            
            pageTemplates.Add(pageTemplate);    
        }
        
        _dbContext.PageTemplates.AddRange(pageTemplates);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();

        return pageTemplates;
    }
    
    public int CountPageTemplates()
    {
        return _dbContext.PageTemplates.Count();
    }
}
