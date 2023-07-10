using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

public interface IPageTemplateRepository : IGenericRepository<PageTemplate>
{
    /// <summary>
    /// Updates a temporary version of a <see cref="PageTemplate"/> with the new state.
    /// This aids in implementing optimistic concurrency, meaning that the update will only be applied if
    /// the <see cref="PageTemplate"/> has not been updated since the last time it was fetched and when a non-temporary
    /// update action is triggered.
    /// </summary>
    /// <param name="entity">The new state of the <see cref="PageTemplate"/></param>
    /// <param name="adminId">The Id of the admin that made the update</param>
    /// <returns></returns>
    Task<PageTemplate> TempUpdateAsync(PageTemplate entity, int adminId);
}
