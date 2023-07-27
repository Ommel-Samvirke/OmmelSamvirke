using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class TextBlockConfiguration : IEntityTypeConfiguration<TextBlock>
{
    public void Configure(EntityTypeBuilder<TextBlock> builder)
    {
        builder.ToTable("TextBlocks");
        builder.Property(p => p.ContentBlockType).IsRequired();
        builder.Property(q => q.Text).IsRequired();
    }
}
