using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IPageRepository : IGenericRepository<Page>, IOptimisticLockingRepository<Page>, IVersionedEntityRepository<Page>
{
    Task<List<Page>> GetByPageTemplateId(int pageTemplateId);
}
