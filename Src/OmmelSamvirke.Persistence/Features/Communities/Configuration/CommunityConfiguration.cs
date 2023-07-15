using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Communities.Models;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Communities.Configuration;

public class CommunityConfiguration : BaseEntityTypeConfiguration<Community>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Community> builder)
    {
        builder.ToTable("Communities");
        builder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnType("nvarchar(100)");
    }
}
