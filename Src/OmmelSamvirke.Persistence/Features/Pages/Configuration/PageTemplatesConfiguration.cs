using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class PageTemplatesConfiguration : BaseEntityTypeConfiguration<PageTemplate>
{
    protected override void ConfigureEntity(EntityTypeBuilder<PageTemplate> builder)
    {
        builder.ToTable("PageTemplates");
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);
    }
}
