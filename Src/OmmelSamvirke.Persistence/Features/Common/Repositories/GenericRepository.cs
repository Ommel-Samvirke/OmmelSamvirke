using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Common;
using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Persistence.DatabaseContext;

namespace OmmelSamvirke.Persistence.Features.Common.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    protected DbSet<T> DbSet { get; }
    protected AppDbContext DbDbContext { get; }

    public GenericRepository(AppDbContext dbDbContext)
    {
        DbDbContext = dbDbContext;
        DbSet = dbDbContext.Set<T>();
    }
    
    public async Task<IReadOnlyList<T>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await DbSet.AsNoTracking().FirstOrDefaultAsync(q => q.Id == id, cancellationToken);
    }

    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await DbDbContext.AddAsync(entity, cancellationToken);
        DbDbContext.Entry(entity).State = EntityState.Added;
        await DbDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbDbContext.Update(entity);
        DbDbContext.Entry(entity).State = EntityState.Modified;
        await DbDbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default)
    {
        try
        {
            DbDbContext.ChangeTracker.Clear();
            DbDbContext.Remove(entity);
            DbDbContext.Entry(entity).State = EntityState.Deleted;
            await DbDbContext.SaveChangesAsync(cancellationToken);
            return true;
        } catch (Exception)
        {
            return false;
        }
    }
}
