using OmmelSamvirke.Domain.Features.Communities.Models;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.TestUtilities.Features.Communities;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Features.Pages.Fixtures;

public class PagesFixture
{
    private readonly AppDbContext _dbContext;

    public PagesFixture(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        ClearTables();
    }

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

    public Page InsertPage()
    {
        Page page = GlobalPageFixtures.DefaultPage();
        _dbContext.Pages.Add(page);
        
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();

        return page;
    }

    public List<IContentBlockData> InsertContentBlockData()
    {
        HeadlineBlockData headlineBlockData = GlobalContentBlockDataFixtures.DefaultContentBlockData();
        
        _dbContext.HeadlineBlockData.Add(headlineBlockData);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        
        return new List<IContentBlockData> { headlineBlockData };
    }
    
    public Community InsertCommunity()
    {
        Community community = GlobalCommunityFixtures.DefaultCommunity();
        _dbContext.Communities.Add(community);
        
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();

        return community;
    }

    private void ClearTables()
    {
        _dbContext.PageTemplates.RemoveRange(_dbContext.PageTemplates);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
    }
}
