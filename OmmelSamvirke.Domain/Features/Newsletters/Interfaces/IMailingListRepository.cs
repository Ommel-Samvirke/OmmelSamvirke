using OmmelSamvirke.Domain.Common.Interfaces;
using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces;

public interface IMailingListRepository : IGenericRepository<MailingList>
{
    Task<bool> AddSubscriber(NewsletterSubscriber newsletterSubscriber);
    Task<bool> RemoveSubscriber(NewsletterSubscriber newsletterSubscriber);
}
