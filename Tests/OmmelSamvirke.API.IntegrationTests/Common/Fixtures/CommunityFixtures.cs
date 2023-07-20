using OmmelSamvirke.Domain.Features.Communities.Models;
using OmmelSamvirke.TestUtilities.Features.Communities;

namespace OmmelSamvirke.API.E2ETests.Common.Fixtures;

public partial class TestFixtures
{
    public Community InsertCommunity()
    {
        Community community = GlobalCommunityFixtures.DefaultCommunity();
        _dbContext.Communities.Add(community);
        
        _dbContext.SaveChanges();
        _dbContext.ChangeTracker.Clear();

        return community;
    }
}
