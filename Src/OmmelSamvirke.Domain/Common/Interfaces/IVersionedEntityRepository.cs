namespace OmmelSamvirke.Domain.Common.Interfaces;

public interface IVersionedEntityRepository<T> where T : BaseModel
{
    /// <summary>
    /// Save a version of <see cref="T"/>.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> SaveVersionAsync(T entity);
}
