using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces;

public interface INewsletterSendingService
{
    public Task SendNow(Newsletter newsletter, NewsletterCommunity newsletterCommunity);
    public Task SendLater(Newsletter newsletter, NewsletterCommunity newsletterCommunity, DateTime sendTime);
    public Task UndoSend(Newsletter newsletter);
}
