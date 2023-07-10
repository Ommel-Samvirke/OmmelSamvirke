using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces.Services;

public interface INewsletterCommentControllerService
{
    Task<NewsletterComment> CreateComment(NewsletterComment newsletterComment);
    Task<NewsletterComment> GetComment(Newsletter newsletter, int commentId);
    Task<IList<NewsletterComment>> GetComments(Newsletter newsletter, IReadOnlySet<int> commentIds);
    Task<IList<NewsletterComment>> GetComments(Newsletter newsletter);
    Task<NewsletterComment> UpdateComment(NewsletterComment newsletterComment);
    Task<bool> DeleteComment(NewsletterComment newsletterComment);
}
