using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class LayoutConfigurationConfiguration : BaseEntityTypeConfiguration<LayoutConfiguration>
{
    protected override void ConfigureEntity(EntityTypeBuilder<LayoutConfiguration> builder)
    {
        builder.ToTable("LayoutConfigurations");
        builder.HasMany(p => p.ContentBlocks)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
