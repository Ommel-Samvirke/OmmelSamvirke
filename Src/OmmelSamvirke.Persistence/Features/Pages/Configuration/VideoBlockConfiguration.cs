using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class VideoBlockConfiguration : IEntityTypeConfiguration<VideoBlock>
{
    public void Configure(EntityTypeBuilder<VideoBlock> builder)
    {
        builder.ToTable("VideoBlocks");
        builder.Property(p => p.ContentBlockType).IsRequired();
        builder.Property(p => p.VideoUrl)
            .IsRequired()
            .HasMaxLength(4_000);
    }
}
