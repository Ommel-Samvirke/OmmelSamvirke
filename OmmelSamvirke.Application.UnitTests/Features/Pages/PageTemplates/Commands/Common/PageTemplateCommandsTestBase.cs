using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;

public abstract class PageTemplateCommandsTestBase
{
    protected Mock<IPageTemplateRepository> PageTemplateRepository = null!;
    protected Mock<IPageRepository> PageRepository = null!;
    protected Mock<IContentBlockDataRepository> ContentBlockDataRepository = null!;
    protected Mock<IMapper> Mapper = null!;
    protected PageTemplate DefaultPageTemplate = null!;
    protected PageTemplate DefaultArchivedPageTemplate = null!;
    protected PageTemplateDto DefaultPageTemplateDto = null!;
    protected Page DefaultPage = null!;
    protected const int DefaultPageTemplateId = 1;

    [SetUp]
    public virtual void SetUp()
    {
        Mapper = new Mock<IMapper>();
        PageTemplateRepository = new Mock<IPageTemplateRepository>();
        PageRepository = new Mock<IPageRepository>();
        ContentBlockDataRepository = new Mock<IContentBlockDataRepository>();
        DefaultPageTemplate = new PageTemplate(
            DefaultPageTemplateId,
            DateTime.Now, 
            DateTime.Now,
            "TestTemplate",
            new HashSet<Layouts>(),
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        DefaultArchivedPageTemplate = new PageTemplate(
            "TestTemplate",
            new HashSet<Layouts>(),
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        DefaultPageTemplateDto = new PageTemplateDto(
            DefaultPageTemplateId,
            "TestTemplate",
            new HashSet<Layouts>(),
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        DefaultPage = new Page(
            1,
            DateTime.Now,
            DateTime.Now,
            "TestPage",
            DefaultPageTemplate
        );
    }
}
