using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Communities.Models;

namespace OmmelSamvirke.Persistence.DatabaseContext;

public sealed partial class AppDbContext
{
    public DbSet<Community> Communities { get; set; } = null!;
}
