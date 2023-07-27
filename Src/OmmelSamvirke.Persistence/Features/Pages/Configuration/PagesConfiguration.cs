using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class PagesConfiguration : BaseEntityTypeConfiguration<Page>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Page> builder)
    {
        builder.ToTable("Pages");
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.State)
            .IsRequired();
        
        builder.HasOne(p => p.DesktopConfiguration)
            .WithOne()
            .HasForeignKey<Page>(p => p.DesktopConfigurationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(p => p.TabletConfiguration)
            .WithOne()
            .HasForeignKey<Page>(p => p.TabletConfigurationId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(p => p.MobileConfiguration)
            .WithOne()
            .HasForeignKey<Page>(p => p.MobileConfigurationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
