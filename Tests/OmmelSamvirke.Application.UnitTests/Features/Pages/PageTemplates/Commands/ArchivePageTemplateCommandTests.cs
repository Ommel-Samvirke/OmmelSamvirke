using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class ArchivePageTemplateCommandTests : PageTemplateCommandsTestBase
{
    private ArchivePageTemplateCommandHandler _archivePageTemplateCommandHandler = null!;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _archivePageTemplateCommandHandler = new ArchivePageTemplateCommandHandler(Mapper.Object, PageTemplateRepository.Object);
    }
    
    [Test]
    public async Task Handle_GivenValidRequest_ShouldArchivePageTemplate()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultArchivedPageTemplate);
        Mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);

        ArchivePageTemplateCommand command = new(DefaultPageTemplateId, PageTemplateState.Public);

        // Act
        PageTemplateDto result = await _archivePageTemplateCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBe(DefaultPageTemplateDto);
        DefaultPageTemplate.State.ShouldBe(PageTemplateState.Archived);
    }

    [Test]
    public void Handle_GivenNonExistentPageTemplate_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PageTemplate)null!);

        ArchivePageTemplateCommand command = new(DefaultPageTemplateId, PageTemplateState.Public);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _archivePageTemplateCommandHandler.Handle(command, CancellationToken.None)
        );
    }

    [Test]
    public void Handle_GivenPageTemplateNotInPublicState_ShouldThrowBadRequestException()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);

        ArchivePageTemplateCommand command = new(DefaultPageTemplateId, PageTemplateState.Archived);

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() =>
            _archivePageTemplateCommandHandler.Handle(command, CancellationToken.None)
        );
    }
    
    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const string exceptionMessage = "Repository failure";
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception(exceptionMessage));
        ArchivePageTemplateCommand command = new(DefaultPageTemplateId, PageTemplateState.Archived);
        
        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _archivePageTemplateCommandHandler.Handle(command, CancellationToken.None)
        );

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
