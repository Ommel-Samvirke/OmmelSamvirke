using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using RemoveContentBlockFromPageTemplateCommand = OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands.RemoveContentBlockFromPageTemplateCommand;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class RemoveContentBlockFromPageTemplateCommandHandlerTests : PageTemplateCommandsTestBase
{
    private RemoveContentBlockFromPageTemplateCommandHandler _removeContentBlockFromPageTemplateCommandHandler = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _removeContentBlockFromPageTemplateCommandHandler = new RemoveContentBlockFromPageTemplateCommandHandler(
            PageTemplateRepository.Object, 
            ContentBlockRepository.Object,
            AdminRepository.Object,
            Mapper.Object
        );
        DefaultPageTemplate.ContentBlocks.Add(DefaultContentBlock);
    }

    [Test]
    public async Task Handle_GivenValidRequest_ShouldRemoveContentBlockFromPageTemplate()
    {
        // Arrange
        ContentBlockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultContentBlock);
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);
        AdminRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultAdmin);
        Mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);

        RemoveContentBlockFromPageTemplateCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto, 1);

        // Act
        PageTemplateDto result = await _removeContentBlockFromPageTemplateCommandHandler.Handle(command, CancellationToken.None);
    
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(DefaultPageTemplate.ContentBlocks, Does.Not.Contain(DefaultContentBlock));
        });
    }

    [Test]
    public void Handle_GivenNonExistentPageTemplate_ShouldThrowNotFoundException()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultArchivedPageTemplate);

        RemoveContentBlockFromPageTemplateCommand command = new(DefaultArchivedPageTemplateDto, DefaultContentBlockDto, 1);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _removeContentBlockFromPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Handle_GivenContentBlockNotInPageTemplate_ShouldThrowBadRequestException()
    {
        // Arrange
        ContentBlock unrelatedContentBlock = new HeadlineBlock(
            1,
            DateTime.Now,
            DateTime.Now,
            false,
            DefaultContentBlockLayoutConfiguration,
            DefaultContentBlockLayoutConfiguration,
            DefaultContentBlockLayoutConfiguration
        );
        ContentBlockDto unrelatedContentBlockDto = new(
            1,
            false,
            DefaultContentBlockLayoutConfigurationDto,
            DefaultContentBlockLayoutConfigurationDto,
            DefaultContentBlockLayoutConfigurationDto,
            ContentBlockType.PdfBlock
        );
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        ContentBlockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(unrelatedContentBlock);
        AdminRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultAdmin);

        RemoveContentBlockFromPageTemplateCommand command = new(DefaultPageTemplateDto, unrelatedContentBlockDto, 1);

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() =>
            _removeContentBlockFromPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const string exceptionMessage = "Repository failure";
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception(exceptionMessage));

        RemoveContentBlockFromPageTemplateCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto, 1);

        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _removeContentBlockFromPageTemplateCommandHandler.Handle(command, CancellationToken.None));

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
