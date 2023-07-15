using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Common;

namespace OmmelSamvirke.Persistence.Features.Common.Configuration;

public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseModel
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).ValueGeneratedOnAdd();
        builder.Property(q => q.DateCreated).IsRequired();
        builder.Property(q => q.DateModified).IsRequired();
        
        ConfigureEntity(builder);
    }
    
    protected virtual void ConfigureEntity(EntityTypeBuilder<T> builder)
    {
        
    }
}
