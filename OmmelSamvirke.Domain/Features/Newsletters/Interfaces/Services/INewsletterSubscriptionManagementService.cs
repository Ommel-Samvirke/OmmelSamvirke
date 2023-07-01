using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces.Services;

public interface INewsletterSubscriptionManagementService
{
    Task<bool> Subscribe(NewsletterSubscriber newsletterSubscriber, NewsletterCommunity newsletterCommunity);
    Task<bool> Subscribe(NewsletterSubscriber newsletterSubscriber, IReadOnlySet<NewsletterCommunity> newsletterCommunities);
    Task<bool> SubscribeAll(NewsletterSubscriber newsletterSubscriber);
    Task<bool> Unsubscribe(NewsletterSubscriber newsletterSubscriber, NewsletterCommunity newsletterCommunity);
    Task<bool> Unsubscribe(NewsletterSubscriber newsletterSubscriber, IReadOnlySet<NewsletterCommunity> newsletterCommunity);
    Task<bool> UnsubscribeAll(NewsletterSubscriber newsletterSubscriber);
}
