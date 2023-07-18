using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class PagesConfiguration : BaseEntityTypeConfiguration<Page>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Page> builder)
    {
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.State)
            .IsRequired();
        
        builder.HasOne<PageTemplate>()
            .WithMany()
            .HasForeignKey(p => p.TemplateId)
            .IsRequired();
    }
}

