using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class HeadlineBlockConfiguration : IEntityTypeConfiguration<HeadlineBlock>
{
    public void Configure(EntityTypeBuilder<HeadlineBlock> builder)
    {
        builder.ToTable("HeadlineBlocks");
        builder.Property(p => p.ContentBlockType).IsRequired();
        builder.Property(p => p.Headline)
            .IsRequired()
            .HasMaxLength(200);
    }
}
