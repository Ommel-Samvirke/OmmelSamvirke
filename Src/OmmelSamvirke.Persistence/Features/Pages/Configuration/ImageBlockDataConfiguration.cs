using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class ImageBlockDataConfiguration : BaseEntityTypeConfiguration<ImageBlockData>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ImageBlockData> builder)
    {
        builder.ToTable("ImageBlockData");
        builder.Property(p => p.ImageUrl)
            .IsRequired()
            .HasMaxLength(4_000);
        builder.HasOne(q => q.ContentBlock)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
