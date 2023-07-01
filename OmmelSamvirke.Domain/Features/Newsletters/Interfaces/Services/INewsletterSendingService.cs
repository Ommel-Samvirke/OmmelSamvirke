using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces.Services;

public interface INewsletterSendingService
{ 
     Task<bool> SendNow(Newsletter newsletter, NewsletterCommunity newsletterCommunity);
     Task<bool> SendLater(Newsletter newsletter, NewsletterCommunity newsletterCommunity, DateTime sendTime);
     Task<bool> UndoSend(Newsletter newsletter);
}
