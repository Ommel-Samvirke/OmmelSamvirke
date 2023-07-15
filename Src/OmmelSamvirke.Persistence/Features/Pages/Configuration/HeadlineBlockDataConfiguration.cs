using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class HeadlineBlockDataConfiguration : BaseEntityTypeConfiguration<HeadlineBlockData>
{
    protected override void ConfigureEntity(EntityTypeBuilder<HeadlineBlockData> builder)
    {
        builder.ToTable("HeadlineBlockData");
        builder.Property(q => q.Headline)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasOne(q => q.ContentBlock)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
