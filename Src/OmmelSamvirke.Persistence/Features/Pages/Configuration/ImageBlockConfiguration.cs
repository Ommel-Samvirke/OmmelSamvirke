using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class ImageBlockConfiguration : IEntityTypeConfiguration<ImageBlock>
{
    public void Configure(EntityTypeBuilder<ImageBlock> builder)
    {
        builder.ToTable("ImageBlocks");
        builder.Property(p => p.ContentBlockType).IsRequired();
        builder.Property(p => p.ImageUrl)
            .IsRequired()
            .HasMaxLength(4_000);
    }
}
