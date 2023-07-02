﻿namespace OmmelSamvirke.Domain.Common.Interfaces;

public interface IGenericRepository<T> where T : BaseModel
{
    Task<IReadOnlyList<T>> GetAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<bool> DeleteAsync(T entity);
}
