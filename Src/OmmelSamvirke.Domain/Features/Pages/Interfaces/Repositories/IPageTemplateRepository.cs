using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IPageTemplateRepository : IGenericRepository<PageTemplate>
{
    Task<PageTemplate?> GetByIdAsyncWithNavigationProps(int id, CancellationToken cancellationToken = default);
}
