using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class PdfBlockDataConfiguration : BaseEntityTypeConfiguration<PdfBlockData>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PdfBlockData> builder)
    {
        builder.ToTable("PdfBlockData");
        builder.Property(p => p.PdfUrl)
            .IsRequired()
            .HasMaxLength(4_000);
        builder.HasOne(q => q.ContentBlock)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
