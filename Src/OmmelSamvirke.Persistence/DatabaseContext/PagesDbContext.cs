using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Persistence.DatabaseContext;

public partial class AppDbContext
{
    public DbSet<Page> Pages { get; set; }
    public DbSet<PageTemplate> PageTemplates { get; set; }
    public DbSet<ContentBlockLayoutConfiguration> ContentBlockLayoutConfigurations { get; set; }
    public DbSet<ContentBlock> ContentBlocks { get; set; }
    public DbSet<HeadlineBlockData> HeadlineBlockData { get; set; }
    public DbSet<ImageBlockData> ImageBlockData { get; set; }
    public DbSet<PdfBlockData> PdfBlockData { get; set; }
    public DbSet<SlideshowBlockData> SlideshowBlockData { get; set; }
    public DbSet<TextBlockData> TextBlockData { get; set; }
    public DbSet<VideoBlockData> VideoBlockData { get; set; }
}
