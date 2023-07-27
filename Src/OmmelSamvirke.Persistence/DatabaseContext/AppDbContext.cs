using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Persistence.DatabaseContext;

public sealed partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (EntityEntry<BaseModel> entry in ChangeTracker.Entries<BaseModel>().Where(
                     q => q.State is EntityState.Added or EntityState.Modified)
                )
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
                entry.Entity.DateModified = entry.Entity.DateCreated;
            }
            else
            {
                entry.Property(nameof(BaseModel.DateCreated)).IsModified = false;
                entry.Entity.DateModified = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
