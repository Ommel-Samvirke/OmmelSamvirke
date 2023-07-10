using OmmelSamvirke.Domain.Features.ContentSharing.Models;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Services;

public interface IPageService
{
    Task<Page> CreatePage(string name, int templateId);
    Task<Page> UpdatePage(int id, string name, int templateId);
    Task<bool> DeletePage(int id);
    Task<bool> ShareOnFacebook(int id, FacebookShareOptions options);
}
