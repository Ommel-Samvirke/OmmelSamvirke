using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces;

public interface INewsletterSubscriptionManagementService
{
    public Task Subscribe(NewsletterSubscriber newsletterSubscriber, NewsletterCommunity newsletterCommunity);
    public Task Subscribe(NewsletterSubscriber newsletterSubscriber, IReadOnlySet<NewsletterCommunity> newsletterCommunities);
    public Task SubscribeAll(NewsletterSubscriber newsletterSubscriber);
    public Task Unsubscribe(NewsletterSubscriber newsletterSubscriber, NewsletterCommunity newsletterCommunity);
    public Task Unsubscribe(NewsletterSubscriber newsletterSubscriber, IReadOnlySet<NewsletterCommunity> newsletterCommunity);
    public Task UnsubscribeAll(NewsletterSubscriber newsletterSubscriber);
}
