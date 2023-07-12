using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class DeletePageTemplateCommandTests : PageTemplateCommandsTestBase
{
    private DeletePageTemplateCommandHandler _deletePageTemplateCommandHandler = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _deletePageTemplateCommandHandler = new DeletePageTemplateCommandHandler(
            PageTemplateRepository.Object,
            PageRepository.Object
        );
    }
    
    [Test]
    public async Task Handle_GivenValidRequest_ShouldReturnTrue()
    {
        // Arrange
        PageRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Page>());
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(DefaultPageTemplateId)).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.DeleteAsync(DefaultPageTemplate)).ReturnsAsync(true);
        PageRepository.Setup(repo => repo.GetByPageTemplateId(It.IsAny<int>())).ReturnsAsync(new List<Page>());
        DeletePageTemplateCommand command = new(DefaultPageTemplateId);

        // Act
        bool result = await _deletePageTemplateCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        PageTemplateRepository.Verify(repo => repo.DeleteAsync(DefaultPageTemplate), Times.Once);
    }
    
    [Test]
    public void Handle_GivenPageTemplateDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        PageRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Page>());
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(DefaultPageTemplateId)).ReturnsAsync((PageTemplate?)null);
        PageTemplateRepository.Setup(repo => repo.DeleteAsync(DefaultPageTemplate)).ReturnsAsync(false);
        DeletePageTemplateCommand command = new(DefaultPageTemplateId);
        
        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(async () =>
            await _deletePageTemplateCommandHandler.Handle(command, CancellationToken.None)
        );
    }
    
    [Test]
    public void Handle_GivenPageTemplateIsInUse_ShouldThrowResourceInUseException()
    {
        // Arrange
        PageRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Page> { new("TestPage", DefaultPageTemplate, 1) });
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(DefaultPageTemplateId)).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.DeleteAsync(DefaultPageTemplate)).ReturnsAsync(true);
        DeletePageTemplateCommand command = new(DefaultPageTemplateId);
        
        // Act / Assert
        Assert.ThrowsAsync<ResourceInUseException>(async () =>
            await _deletePageTemplateCommandHandler.Handle(command, CancellationToken.None)
        );
    }
    
    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const string exceptionMessage = "Repository failure";

        PageRepository.Setup(repo => repo.GetAsync()).ThrowsAsync(new Exception(exceptionMessage));
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(DefaultPageTemplateId)).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.DeleteAsync(DefaultPageTemplate)).ReturnsAsync(true);
        DeletePageTemplateCommand command = new(DefaultPageTemplateId);

        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _deletePageTemplateCommandHandler.Handle(command, CancellationToken.None)
        );

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
