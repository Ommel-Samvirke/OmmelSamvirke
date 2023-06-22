using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces;

public interface INewsletterCommunityAssociationsRepository : IGenericRepository<NewsletterCommunityAssociations>
{
    Task<bool> AddNewsletterCommunity(NewsletterCommunity newsletterCommunity);
}
