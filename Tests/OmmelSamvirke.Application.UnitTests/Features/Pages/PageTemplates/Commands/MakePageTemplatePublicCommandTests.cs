using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class MakePageTemplatePublicCommandTests : PageTemplateCommandsTestBase
{
    private MakePageTemplatePublicCommandHandler _makePageTemplatePublicCommandHandler = null!;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _makePageTemplatePublicCommandHandler = new MakePageTemplatePublicCommandHandler(Mapper.Object, PageTemplateRepository.Object);
    }
    
    [Test]
    public async Task Handle_GivenValidRequest_ShouldArchivePageTemplate()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultArchivedPageTemplate);
        PageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);
        Mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);

        MakePageTemplatePublicCommand command = new(DefaultPageTemplateId, PageTemplateState.Archived);

        // Act
        PageTemplateDto result = await _makePageTemplatePublicCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBe(DefaultPageTemplateDto);
        DefaultArchivedPageTemplate.State.ShouldBe(PageTemplateState.Public);
    }

    [Test]
    public void Handle_GivenNonExistentPageTemplate_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PageTemplate)null!);

        MakePageTemplatePublicCommand command = new(DefaultPageTemplateId, PageTemplateState.Public);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _makePageTemplatePublicCommandHandler.Handle(command, CancellationToken.None)
        );
    }

    [Test]
    public void Handle_GivenPageTemplateInPublicState_ShouldThrowBadRequestException()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultArchivedPageTemplate);

        MakePageTemplatePublicCommand command = new(DefaultPageTemplateId, PageTemplateState.Public);

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() =>
            _makePageTemplatePublicCommandHandler.Handle(command, CancellationToken.None)
        );
    }
    
    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const string exceptionMessage = "Repository failure";
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception(exceptionMessage));
        MakePageTemplatePublicCommand command = new(DefaultPageTemplateId, PageTemplateState.Archived);
        
        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _makePageTemplatePublicCommandHandler.Handle(command, CancellationToken.None)
        );

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
