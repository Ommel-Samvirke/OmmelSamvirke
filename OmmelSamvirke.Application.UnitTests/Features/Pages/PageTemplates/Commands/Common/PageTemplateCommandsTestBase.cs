using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Domain.Features.Admins.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Admins.Models;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;

public abstract class PageTemplateCommandsTestBase
{
    protected Mock<IPageTemplateRepository> PageTemplateRepository = null!;
    protected Mock<IPageRepository> PageRepository = null!;
    protected Mock<IContentBlockRepository> ContentBlockRepository = null!;
    protected Mock<IContentBlockDataRepository> ContentBlockDataRepository = null!;
    protected Mock<IAdminRepository> AdminRepository = null!;
    protected Mock<IMapper> Mapper = null!;
    
    protected PageTemplate DefaultPageTemplate = null!;
    protected PageTemplateDto DefaultPageTemplateDto = null!;
    protected PageTemplate DefaultArchivedPageTemplate = null!;
    protected PageTemplateDto DefaultArchivedPageTemplateDto = null!;
    protected Page DefaultPage = null!;
    protected PageDto DefaultPageDto = null!;
    protected ContentBlockLayoutConfiguration DefaultContentBlockLayoutConfiguration { get; private set; } = null!;
    protected ContentBlockLayoutConfigurationDto DefaultContentBlockLayoutConfigurationDto { get; private set; } = null!;
    protected HeadlineBlock DefaultContentBlock { get; private set; } = null!;
    protected ContentBlockDto DefaultContentBlockDto { get; private set; } = null!;
    protected Admin DefaultAdmin { get; private set; } = null!;
    protected const int DefaultPageTemplateId = 1;

    [SetUp]
    public virtual void SetUp()
    {
        DateTime now = DateTime.Now;
        Mapper = new Mock<IMapper>();
        
        PageTemplateRepository = new Mock<IPageTemplateRepository>();
        PageRepository = new Mock<IPageRepository>();
        ContentBlockRepository = new Mock<IContentBlockRepository>();
        ContentBlockDataRepository = new Mock<IContentBlockDataRepository>();
        AdminRepository = new Mock<IAdminRepository>();
        
        DefaultPageTemplate = new PageTemplate(
            DefaultPageTemplateId,
            now, 
            now,
            "TestTemplate",
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        DefaultPageTemplateDto = new PageTemplateDto(
            DefaultPageTemplateId,
            "TestTemplate",
            new List<ContentBlockDto>(),
            PageTemplateState.Public
        );
        
        DefaultArchivedPageTemplate = new PageTemplate(
            10,
            now,
            now,
            "TestTemplate",
            new List<ContentBlock>(),
            PageTemplateState.Archived
        );
        DefaultArchivedPageTemplateDto = new PageTemplateDto(
            10,
            "TestTemplate",
            new List<ContentBlockDto>(),
            PageTemplateState.Archived
        );

        DefaultPage = new Page(
            1,
            now,
            now,
            "TestPage",
            DefaultPageTemplate
        );
        DefaultPageDto = new PageDto(
            1,
            "TestPage",
            DefaultPageTemplateDto
        );
        
        DefaultContentBlockLayoutConfiguration = new ContentBlockLayoutConfiguration(
            1,
            now,
            now,
            0,
            0,
            1
        );
        DefaultContentBlockLayoutConfigurationDto = new ContentBlockLayoutConfigurationDto(
            1,
            0,
            1,
            1,
            1
        );
        
        DefaultContentBlock = new HeadlineBlock(
            1,
            DateTime.Now,
            DateTime.Now,
            false,
            DefaultContentBlockLayoutConfiguration,
            DefaultContentBlockLayoutConfiguration,
            DefaultContentBlockLayoutConfiguration
        );
        DefaultContentBlockDto = new ContentBlockDto(
            1,
            false,
            DefaultContentBlockLayoutConfigurationDto,
            DefaultContentBlockLayoutConfigurationDto,
            DefaultContentBlockLayoutConfigurationDto,
            ContentBlockType.HeadlineBlock
        );

        DefaultAdmin = new Admin(1, now, now);
    }
}
