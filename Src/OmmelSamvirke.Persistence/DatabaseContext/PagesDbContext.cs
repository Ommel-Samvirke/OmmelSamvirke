using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Persistence.DatabaseContext;

public sealed partial class AppDbContext
{
    public DbSet<Page> Pages { get; set; } = null!;
    public DbSet<LayoutConfiguration> LayoutConfigurations { get; set; } = null!;
    public DbSet<ContentBlock> ContentBlocks { get; set; } = null!;
}
