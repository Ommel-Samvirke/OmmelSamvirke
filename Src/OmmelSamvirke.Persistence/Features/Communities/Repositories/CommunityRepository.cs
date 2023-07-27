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

    public async Task<Page> GetNextPage(int communityId, int currentPageId, CancellationToken cancellationToken = default)
    {
        Community? community = await GetCommunityWithPages(communityId, cancellationToken);
        List<Page> sortedPages = OrderPagesById(community);

        int currentPageIndex = GetPageIndex(currentPageId, sortedPages, "Current page not found");

        return currentPageIndex == sortedPages.Count - 1 ? sortedPages[0] : sortedPages[currentPageIndex + 1];
    }

    public async Task<Page> GetPreviousPage(int communityId, int currentPageId, CancellationToken cancellationToken = default)
    {
        Community? community = await GetCommunityWithPages(communityId, cancellationToken);
        List<Page> sortedPages = OrderPagesById(community);

        int currentPageIndex = GetPageIndex(currentPageId, sortedPages, "Current page not found");

        return currentPageIndex == 0 ? sortedPages[^1] : sortedPages[currentPageIndex - 1];
    }

    public async Task<List<Page>> GetPages(int communityId, CancellationToken cancellationToken = default)
    {
        Community? community = await GetCommunityWithPages(communityId, cancellationToken);
        
        return OrderPagesById(community);
    }

    private async Task<Community?> GetCommunityWithPages(int communityId, CancellationToken cancellationToken = default)
    {
        Community? community = await _dbSet.Include(e => e.Pages).FirstOrDefaultAsync(e => 
            e.Id == communityId,
            cancellationToken
        );

        return community;
    }

    private static List<Page> OrderPagesById(Community? community)
    {
        if (community is null)
            return new List<Page>();
        
        return community.Pages.OrderBy(p => p.Id).ToList();
    }

    private static int GetPageIndex(int pageId, List<Page> pages, string errorMessage)
    {
        int pageIndex = pages.FindIndex(p => p.Id == pageId);

        if (pageIndex == -1)
        {
            throw new Exception(errorMessage);
        }

        return pageIndex;
    }

    public override async Task<IReadOnlyList<Community>> GetWithRelationsAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.Pages)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public override async Task<Community?> GetByIdWithRelationsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(p => p.Pages)
            .Where(p => p.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
}
