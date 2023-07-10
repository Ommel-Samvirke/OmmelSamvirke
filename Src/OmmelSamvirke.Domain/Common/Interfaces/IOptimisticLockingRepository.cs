namespace OmmelSamvirke.Domain.Common.Interfaces;

public interface IOptimisticLockingRepository<T> where T : BaseModel
{
    /// <summary>
    /// Updates a temporary version of <see cref="T"/> with the new state.
    /// This aids in implementing optimistic concurrency, meaning that the update will only be applied if
    /// <see cref="T"/> has not been updated since the last time it was fetched and when a non-temporary
    /// update action is triggered.
    /// </summary>
    /// <param name="entity">The new state of the <see cref="T"/></param>
    /// <param name="adminId">The Id of the admin that made the update</param>
    /// <returns></returns>
    Task<T> TempUpdateAsync(T entity, int adminId);
    
    /// <summary>
    /// Gets a temporarily saved entity <see cref="T"/> by its Id.
    /// </summary>
    /// <param name="id">The Id of the <see cref="T"/></param>
    /// <returns></returns>
    Task<T> GetTempByIdAsync(int id);
}
