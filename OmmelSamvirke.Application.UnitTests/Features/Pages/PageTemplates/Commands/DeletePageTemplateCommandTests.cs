using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class DeletePageTemplateCommandTests
{
    private Mock<IPageTemplateRepository> _pageTemplateRepository = null!;
    private Mock<IPageRepository> _pageRepository = null!;
    private DeletePageTemplateCommandHandler _deletePageTemplateCommandHandler = null!;

    [SetUp]
    public void SetUp()
    {
        _pageTemplateRepository = new Mock<IPageTemplateRepository>();
        _pageRepository = new Mock<IPageRepository>();
        _deletePageTemplateCommandHandler = new DeletePageTemplateCommandHandler(
            _pageTemplateRepository.Object,
            _pageRepository.Object
        );
    }
    
    [Test]
    public async Task Handle_GivenValidRequest_ShouldReturnTrue()
    {
        // Arrange
        const int pageTemplateId = 1;
        PageTemplate pageTemplate = new(
            1,
            DateTime.Now,
            DateTime.Now, 
            "TestTemplate",
            new HashSet<Layouts>(),
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        _pageRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Page>());
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(pageTemplateId)).ReturnsAsync(pageTemplate);
        _pageTemplateRepository.Setup(repo => repo.DeleteAsync(pageTemplate)).ReturnsAsync(true);
        DeletePageTemplateCommand command = new(pageTemplateId);

        // Act
        bool result = await _deletePageTemplateCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        _pageTemplateRepository.Verify(repo => repo.DeleteAsync(pageTemplate), Times.Once);
    }
    
    [Test]
    public void Handle_GivenPageTemplateDoesNotExist_ShouldThrowNotFoundException()
    {
        // Arrange
        const int pageTemplateId = 1;
        PageTemplate pageTemplate = new(
            1,
            DateTime.Now,
            DateTime.Now, 
            "TestTemplate",
            new HashSet<Layouts>(),
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        _pageRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Page>());
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(pageTemplateId)).ReturnsAsync((PageTemplate?)null);
        _pageTemplateRepository.Setup(repo => repo.DeleteAsync(pageTemplate)).ReturnsAsync(false);
        DeletePageTemplateCommand command = new(pageTemplateId);

        // Act
        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(async () =>
            await _deletePageTemplateCommandHandler.Handle(command, CancellationToken.None)
        );
    }
    
    [Test]
    public void Handle_GivenPageTemplateIsInUse_ShouldThrowResourceInUseException()
    {
        // Arrange
        const int pageTemplateId = 1;
        PageTemplate pageTemplate = new(
            1,
            DateTime.Now,
            DateTime.Now, 
            "TestTemplate",
            new HashSet<Layouts>(),
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        _pageRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Page> { new("TestPage", pageTemplate) });
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(pageTemplateId)).ReturnsAsync(pageTemplate);
        _pageTemplateRepository.Setup(repo => repo.DeleteAsync(pageTemplate)).ReturnsAsync(true);
        DeletePageTemplateCommand command = new(pageTemplateId);

        // Act
        // Act / Assert
        Assert.ThrowsAsync<ResourceInUseException>(async () =>
            await _deletePageTemplateCommandHandler.Handle(command, CancellationToken.None)
        );
    }
    
    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const int pageTemplateId = 1;
        const string exceptionMessage = "Repository failure";
        PageTemplate pageTemplate = new(
            1,
            DateTime.Now,
            DateTime.Now, 
            "TestTemplate",
            new HashSet<Layouts>(),
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        _pageRepository.Setup(repo => repo.GetAsync()).ThrowsAsync(new Exception(exceptionMessage));
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(pageTemplateId)).ReturnsAsync(pageTemplate);
        _pageTemplateRepository.Setup(repo => repo.DeleteAsync(pageTemplate)).ReturnsAsync(true);
        DeletePageTemplateCommand command = new(pageTemplateId);

        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _deletePageTemplateCommandHandler.Handle(command, CancellationToken.None)
        );

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
