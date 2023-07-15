using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.ValueObjects;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Pages.Configuration;

public class SlideshowBlockDataConfiguration : BaseEntityTypeConfiguration<SlideshowBlockData>
{
    protected override void ConfigureEntity(EntityTypeBuilder<SlideshowBlockData> builder)
    {
        builder.ToTable("SlideshowBlockData");
        builder.Property(q => q.ImageUrls)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<Url>>(v) ?? new List<Url>()
            ).Metadata.SetValueComparer(new ValueComparer<List<Url>>(
                (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList())
            );
        builder.HasOne(q => q.ContentBlock)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
