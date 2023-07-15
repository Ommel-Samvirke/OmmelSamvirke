using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Interfaces;

namespace OmmelSamvirke.Persistence.Features.Common.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    protected readonly DbContext DbContext;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(DbContext dbContext)
    {
        DbContext = dbContext;
        _dbSet = dbContext.Set<T>();
    }
    
    public async Task<IReadOnlyList<T>> GetAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await DbContext.AddAsync(entity);
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        DbContext.Update(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        try
        {
            DbContext.Remove(entity);
            await DbContext.SaveChangesAsync();
            return true;
        } catch (Exception)
        {
            return false;
        }
    }
}
