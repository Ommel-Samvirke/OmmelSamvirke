namespace OmmelSamvirke.Domain.Common.Interfaces;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<IReadOnlyList<T>> GetAsync(CancellationToken cancellationToken = default);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default);
}
