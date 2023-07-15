using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmmelSamvirke.Domain.Features.Admins.Models;
using OmmelSamvirke.Persistence.Features.Common.Configuration;

namespace OmmelSamvirke.Persistence.Features.Admins.Configuration;

public class AdminsConfiguration : BaseEntityTypeConfiguration<Admin>
{
    protected override void ConfigureEntity(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable("Admins");
    }
}
