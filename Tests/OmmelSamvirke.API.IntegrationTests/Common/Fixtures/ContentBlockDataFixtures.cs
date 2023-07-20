using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Common.Fixtures;

public partial class TestFixtures
{
    public List<IContentBlockData> InsertContentBlockData()
    {
        HeadlineBlockData headlineBlockData = GlobalContentBlockDataFixtures.DefaultContentBlockData();
        
        _dbContext.HeadlineBlockData.Add(headlineBlockData);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        
        return new List<IContentBlockData> { headlineBlockData };
    }
}
