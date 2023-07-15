using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class TextBlockDataConfiguration : BaseEntityTypeConfiguration<TextBlockData>
{
    protected override void ConfigureEntity(EntityTypeBuilder<TextBlockData> builder)
    {
        builder.ToTable("TextBlockData");
        builder.Property(q => q.Text).IsRequired();
        builder.HasOne(q => q.ContentBlock)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
