using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class PdfBlockConfiguration : IEntityTypeConfiguration<PdfBlock>
{
    public void Configure(EntityTypeBuilder<PdfBlock> builder)
    {
        builder.ToTable("PdfBlocks");
        builder.Property(p => p.ContentBlockType).IsRequired();
        builder.Property(p => p.PdfUrl)
            .IsRequired()
            .HasMaxLength(4_000);
    }
}
