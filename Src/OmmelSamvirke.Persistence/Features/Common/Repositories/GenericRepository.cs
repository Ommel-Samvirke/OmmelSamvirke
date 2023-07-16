using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Interfaces;

namespace OmmelSamvirke.Persistence.Features.Common.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    private readonly DbContext _dbDbContext;
    private readonly DbSet<T> _dbSet;

    public DbSet<T> DbSet => _dbSet;
    public DbContext DbDbContext => _dbDbContext;

    public GenericRepository(DbContext dbDbContext)
    {
        _dbDbContext = dbDbContext;
        _dbSet = dbDbContext.Set<T>();
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
        await _dbDbContext.AddAsync(entity);
        _dbDbContext.Entry(entity).State = EntityState.Added;
        await _dbDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbDbContext.Update(entity);
        _dbDbContext.Entry(entity).State = EntityState.Modified;
        await _dbDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        try
        {
            _dbDbContext.ChangeTracker.Clear();
            _dbDbContext.Remove(entity);
            _dbDbContext.Entry(entity).State = EntityState.Deleted;
            await _dbDbContext.SaveChangesAsync();
            return true;
        } catch (Exception)
        {
            return false;
        }
    }
}
