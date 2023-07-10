using OmmelSamvirke.Domain.Features.Newsletters.Models;

namespace OmmelSamvirke.Domain.Features.Newsletters.Interfaces.Services;

public interface INewsletterControllerService
{
    Task<Newsletter> CreateNewsletter(Newsletter newsletter);
    Task<IReadOnlyList<Newsletter>> CreateNewsletters(IReadOnlySet<Newsletter> newsletter);
    Task<Newsletter> GetNewsletter(int id);
    Task<IList<Newsletter>> GetNewsletters(IReadOnlySet<int> ids);
    Task<IList<Newsletter>> GetAllNewsletters();
    Task<Newsletter> UpdateNewsletter(Newsletter newsletter);
    Task<bool> DeleteNewsletter(Newsletter newsletter);
    Task<bool> DeleteNewsletters(IReadOnlySet<Newsletter> newsletters);
    Task<Newsletter> Like(int userId, Newsletter newsletter); // Todo - Review after implementing User feature
    Task<Newsletter> RemoveLike(int userId, Newsletter newsletter); // Todo - Review after implementing User feature
}
