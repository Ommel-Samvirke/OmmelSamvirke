using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface ILayoutConfigurationRepository : IGenericRepository<LayoutConfiguration>
{
    Task<bool> DeleteByPageAsync(Page page, CancellationToken cancellationToken = default);
}
