using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces;

public interface INewsletterAnalyticsService
{
    public Task GetSentNewslettersCount();
    public Task GetSentNewslettersCount(NewsletterCommunity newsletterCommunity);
    public Task GetSubscribersCount();
    public Task GetSubscribersCount(NewsletterCommunity newsletterCommunity);
    public Task GetDeliveryStats();
    public Task GetDeliveryStats(NewsletterCommunity newsletterCommunity);
}
