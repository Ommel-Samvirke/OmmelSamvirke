namespace OmmelSamvirke.Domain.Common.Interfaces;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<IReadOnlyList<T>> GetAsync();
    Task<T?> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
