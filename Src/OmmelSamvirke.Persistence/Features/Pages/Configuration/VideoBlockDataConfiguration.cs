using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class VideoBlockDataConfiguration : BaseEntityTypeConfiguration<VideoBlockData>
{
    protected override void ConfigureEntity(EntityTypeBuilder<VideoBlockData> builder)
    {
        builder.ToTable("VideoBlockData");
        builder.Property(p => p.VideoUrl)
            .IsRequired()
            .HasMaxLength(4_000);
        builder.HasOne(q => q.ContentBlock)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
