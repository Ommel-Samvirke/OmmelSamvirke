using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.Persistence.Features.Common.Repositories;

public abstract class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    protected DbSet<T> DbSet { get; }
    protected AppDbContext DbContext { get; }

    protected GenericRepository(AppDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = dbContext.Set<T>();
    }
    
    public async Task<IReadOnlyList<T>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public abstract Task<IReadOnlyList<T>> GetWithRelationsAsync(CancellationToken cancellationToken = default);

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public abstract Task<T?> GetByIdWithRelationsAsync(int id, CancellationToken cancellationToken = default);

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbContext.AddAsync(entity, cancellationToken);
        DbContext.Entry(entity).State = EntityState.Added;
        await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Update(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.Remove(entity);
        DbContext.Entry(entity).State = EntityState.Deleted;
        await DbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return DbSet.AnyAsync(q => q.Id == id, cancellationToken);
    }

    public Task<bool> IsPropertyUniqueAsync(string propertyName, string propertyValue, CancellationToken cancellationToken = default)
    {
        return DbSet.AllAsync(q => !EF.Property<string>(q, propertyName).Equals(propertyValue), cancellationToken);
    }
}
