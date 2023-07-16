using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class ContentBlockConfiguration : BaseEntityTypeConfiguration<ContentBlock>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ContentBlock> builder)
    {
        builder.ToTable("ContentBlocks");
        builder.Property(q => q.IsOptional).IsRequired();
        
        builder.HasOne(q => q.DesktopConfiguration)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(q => q.TabletConfiguration)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(q => q.MobileConfiguration)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
