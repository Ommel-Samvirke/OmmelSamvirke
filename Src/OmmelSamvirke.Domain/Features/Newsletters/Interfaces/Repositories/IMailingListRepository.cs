using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces.Repositories;

public interface IMailingListRepository : IGenericRepository<MailingList>
{
    Task<NewsletterSubscriber> AddSubscriber(NewsletterSubscriber newsletterSubscriber, CancellationToken cancellationToken = default);
    Task<NewsletterSubscriber> RemoveSubscriber(NewsletterSubscriber newsletterSubscriber, CancellationToken cancellationToken = default);
}
