using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.Features.Pages.Interfaces.Services;

public interface IPageTemplateService
{
    Task<PageTemplate> CreatePageTemplate(string name, IList<Layouts> supportedLayouts, IList<ContentBlock> blocks);
    Task<PageTemplate> UpdatePageTemplate(int id, string name, IList<Layouts> supportedLayouts, IList<ContentBlock> blocks);
    Task<bool> DeletePageTemplate(int id);
    Task<bool> ArchivePageTemplate(int id);
    Task<bool> CheckIfTemplateCanBeDeleted(int id);
    Task<IEnumerable<PageTemplate>> GetTemplatesByState(PageTemplateState state);
}
