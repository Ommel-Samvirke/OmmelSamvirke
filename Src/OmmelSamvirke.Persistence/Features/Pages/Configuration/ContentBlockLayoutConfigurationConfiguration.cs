using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class ContentBlockLayoutConfigurationConfiguration : BaseEntityTypeConfiguration<ContentBlockLayoutConfiguration>
{
    protected override void ConfigureEntity(EntityTypeBuilder<ContentBlockLayoutConfiguration> builder)
    {
        builder.ToTable("ContentBlockLayoutConfigurations");
        builder.Property(q => q.Height).IsRequired();
        builder.Property(q => q.Width).IsRequired();
        builder.Property(q => q.XPosition).IsRequired();
        builder.Property(q => q.YPosition).IsRequired();
    }
}
