using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces;

public interface INewsletterControllerService
{
    public Task CreateNewsletter(Newsletter newsletter);
    public Task CreateNewsletters(IReadOnlySet<Newsletter> newsletter);
    public Task GetNewsletter(int id);
    public Task GetNewsletters(IReadOnlySet<int> ids);
    public Task GetAllNewsletters();
    public Task UpdateNewsletter(Newsletter newsletter);
    public Task DeleteNewsletter(Newsletter newsletter);
    public Task DeleteNewsletters(IReadOnlySet<Newsletter> newsletters);
    public Task Like(int userId, Newsletter newsletter); // Todo - Review after implementing User feature
    public Task RemoveLike(int userId, Newsletter newsletter); // Todo - Review after implementing User feature
}
