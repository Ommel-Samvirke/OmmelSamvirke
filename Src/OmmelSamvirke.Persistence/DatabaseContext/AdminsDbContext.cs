using Microsoft.EntityFrameworkCore;
using OmmelSamvirke.Domain.Features.Admins.Models;

namespace OmmelSamvirke.Persistence.DatabaseContext;

public sealed partial class AppDbContext
{
    public DbSet<Admin> Admins { get; set; } = null!;
}
