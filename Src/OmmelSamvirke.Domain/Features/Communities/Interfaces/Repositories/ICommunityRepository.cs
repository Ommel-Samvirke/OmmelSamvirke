using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Communities.Models;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;

public interface ICommunityRepository : IGenericRepository<Community>
{
    Task<Page> GetNextPage(int communityId, int currentPageId, CancellationToken cancellationToken = default);
    Task<Page> GetPreviousPage(int communityId, int currentPageId, CancellationToken cancellationToken = default);
    Task<List<Page>> GetPages(int communityId, CancellationToken cancellationToken = default);
}
