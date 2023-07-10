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
    
    /// <summary>
    /// Gets a temporarily saved <see cref="PageTemplate"/> by its Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="PageTemplate"/></param>
    /// <returns></returns>
    Task<PageTemplate> GetTempByIdAsync(int id);
    
    /// <summary>
    /// Save a version of a <see cref="PageTemplate"/>.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<PageTemplate> SaveVersionAsync(PageTemplate entity);
}
