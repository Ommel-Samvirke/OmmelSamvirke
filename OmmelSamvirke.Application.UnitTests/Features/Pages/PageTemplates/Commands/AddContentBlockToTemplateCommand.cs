using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class AddContentBlockToPageTemplateCommandHandlerTests : PageTemplateCommandsTestBase
{
    private Mock<IPageTemplateRepository> _pageTemplateRepository = null!;
    private Mock<IContentBlockRepository> _contentBlockRepository = null!;
    private Mock<IMapper> _mapper = null!;
    private AddContentBlockToPageTemplateCommandHandler _addContentBlockToPageTemplateCommandHandler = null!;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _pageTemplateRepository = new Mock<IPageTemplateRepository>();
        _contentBlockRepository = new Mock<IContentBlockRepository>();
        _mapper = new Mock<IMapper>();

        _addContentBlockToPageTemplateCommandHandler = new AddContentBlockToPageTemplateCommandHandler(
            _pageTemplateRepository.Object, 
            _contentBlockRepository.Object, 
            _mapper.Object);
    }

    [Test]
    public async Task Handle_GivenValidRequest_ShouldAddContentBlockToPageTemplate()
    {
        // Arrange
        _contentBlockRepository.Setup(repo => repo.CreateAsync(It.IsAny<ContentBlock>())).ReturnsAsync(DefaultContentBlock);
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        _pageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);
        _mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);

        AddContentBlockToPageTemplateCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto);

        // Act
        PageTemplateDto result = await _addContentBlockToPageTemplateCommandHandler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(DefaultPageTemplate.ContentBlocks, Has.Count.EqualTo(1));
            Assert.That(DefaultPageTemplate.ContentBlocks, Does.Contain(DefaultContentBlock));
        });
    }

    [Test]
    public void Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
    {
        // Arrange
        AddContentBlockToPageTemplateCommand command = new(DefaultPageTemplateDto, null!);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _addContentBlockToPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_GivenNonExistentPageTemplate_ShouldThrowNotFoundException()
    {
        // Arrange
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PageTemplate)null!);

        AddContentBlockToPageTemplateCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _addContentBlockToPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const string exceptionMessage = "Repository failure";
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception(exceptionMessage));

        AddContentBlockToPageTemplateCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto);

        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _addContentBlockToPageTemplateCommandHandler.Handle(command, CancellationToken.None));

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
