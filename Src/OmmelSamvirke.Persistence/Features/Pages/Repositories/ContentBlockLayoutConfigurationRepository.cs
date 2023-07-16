using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Persistence.DatabaseContext;
using OmmelSamvirke.Persistence.Features.Common.Repositories;

namespace OmmelSamvirke.Persistence.Features.Pages.Repositories;

public class ContentBlockLayoutConfigurationRepository : GenericRepository<ContentBlockLayoutConfiguration>, IContentBlockLayoutConfigurationRepository
{
    public ContentBlockLayoutConfigurationRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
