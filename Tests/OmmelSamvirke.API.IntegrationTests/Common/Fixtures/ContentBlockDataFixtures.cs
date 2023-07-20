using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.TestUtilities.Features.Pages;

namespace OmmelSamvirke.API.E2ETests.Common.Fixtures;

public partial class TestFixtures
{
    public List<IContentBlockData> InsertContentBlockData(bool isOptional = false)
    {
        HeadlineBlockData headlineBlockData = GlobalContentBlockDataFixtures.DefaultContentBlockData();
        headlineBlockData.ContentBlock!.IsOptional = isOptional;
        
        _dbContext.HeadlineBlockData.Add(headlineBlockData);
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();
        
        return new List<IContentBlockData> { headlineBlockData };
    }
}
