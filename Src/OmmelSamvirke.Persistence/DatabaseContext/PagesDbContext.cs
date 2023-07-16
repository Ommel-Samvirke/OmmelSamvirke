using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Persistence.DatabaseContext;

public partial class AppDbContext
{
    public DbSet<Page> Pages { get; set; } = null!;
    public DbSet<PageTemplate> PageTemplates { get; set; } = null!;
    public DbSet<ContentBlockLayoutConfiguration> ContentBlockLayoutConfigurations { get; set; } = null!;
    public DbSet<ContentBlock> ContentBlocks { get; set; } = null!;
    public DbSet<HeadlineBlockData> HeadlineBlockData { get; set; } = null!;
    public DbSet<ImageBlockData> ImageBlockData { get; set; } = null!;
    public DbSet<PdfBlockData> PdfBlockData { get; set; } = null!;
    public DbSet<SlideshowBlockData> SlideshowBlockData { get; set; } = null!;
    public DbSet<TextBlockData> TextBlockData { get; set; } = null!;
    public DbSet<VideoBlockData> VideoBlockData { get; set; } = null!;
}
