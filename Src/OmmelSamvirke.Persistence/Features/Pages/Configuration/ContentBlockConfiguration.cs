using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class ContentBlockConfiguration : BaseEntityTypeConfiguration<ContentBlock>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ContentBlock> builder)
    {
        builder.Property(p => p.XPosition).IsRequired();
        builder.Property(p => p.YPosition).IsRequired();
        builder.Property(p => p.Width).IsRequired();
        builder.Property(p => p.Height).IsRequired();
    }
}
