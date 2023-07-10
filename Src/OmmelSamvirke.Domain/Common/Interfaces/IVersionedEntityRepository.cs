namespace OmmelSamvirke.Domain.Common.Interfaces;

public interface IVersionedEntityRepository<T> where T : BaseModel
{
    /// <summary>
    /// Save a version of <see cref="T"/>.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> SaveVersionAsync(T entity);

    /// <summary>
    /// Get a version of entity <see cref="T"/> by id.
    /// </summary>
    /// <param name="id">The id of entity <see cref="T"/></param>
    /// <returns></returns>
    Task<T?> GetVersionAsync(int id);
}
