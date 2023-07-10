using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces.Services;

public interface INewsletterAnalyticsService
{
    Task<int> GetSentNewslettersCount();
    Task<int> GetSentNewslettersCount(NewsletterCommunity newsletterCommunity);
    Task<int> GetSubscribersCount();
    Task<int> GetSubscribersCount(NewsletterCommunity newsletterCommunity);
}
