using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Communities.Models;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Communities.Repositories;

public class CommunityRepository : GenericRepository<Community>, ICommunityRepository
{
    private readonly DbSet<Community> _dbSet;
    
    public CommunityRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbSet = dbContext.Set<Community>();
    }

    public async Task<Page> GetNextPage(int communityId, int currentPageId)
    {
        Community community = await GetCommunityWithPages(communityId);
        List<Page> sortedPages = OrderPagesById(community);

        int currentPageIndex = GetPageIndex(currentPageId, sortedPages, "Current page not found");

        return currentPageIndex == sortedPages.Count - 1 ? sortedPages[0] : sortedPages[currentPageIndex + 1];
    }

    public async Task<Page> GetPreviousPage(int communityId, int currentPageId)
    {
        Community community = await GetCommunityWithPages(communityId);
        List<Page> sortedPages = OrderPagesById(community);

        int currentPageIndex = GetPageIndex(currentPageId, sortedPages, "Current page not found");

        return currentPageIndex == 0 ? sortedPages[^1] : sortedPages[currentPageIndex - 1];
    }

    public async Task<List<Page>> GetPages(int communityId)
    {
        Community community = await GetCommunityWithPages(communityId);
        
        return OrderPagesById(community);
    }

    private async Task<Community> GetCommunityWithPages(int communityId)
    {
        Community? community = await _dbSet.Include(e => e.Pages).FirstOrDefaultAsync(e => e.Id == communityId);

        if (community?.Pages is null)
        {
            throw new Exception("Community or pages is null");
        }

        return community;
    }

    private List<Page> OrderPagesById(Community community)
    {
        return community.Pages.OrderBy(p => p.Id).ToList();
    }

    private int GetPageIndex(int pageId, List<Page> pages, string errorMessage)
    {
        int pageIndex = pages.FindIndex(p => p.Id == pageId);

        if (pageIndex == -1)
        {
            throw new Exception(errorMessage);
        }

        return pageIndex;
    }
}
