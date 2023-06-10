using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces;

public interface INewsletterCommentControllerService
{
    public Task CreateComment(NewsletterComment newsletterComment);
    public Task GetComment(Newsletter newsletter, int commentId);
    public Task GetComments(Newsletter newsletter, IReadOnlySet<int> commentIds);
    public Task GetComments(Newsletter newsletter);
    public Task UpdateComment(NewsletterComment newsletterComment);
    public Task DeleteComment(NewsletterComment newsletterComment);
}
