using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using RemoveContentBlockFromPageTemplateCommand = OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands.RemoveContentBlockFromPageTemplateCommand;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class RemoveContentBlockFromPageTemplateCommandHandlerTests : PageTemplateCommandsTestBase
{
    private Mock<IPageTemplateRepository> _pageTemplateRepository = null!;
    private Mock<IContentBlockRepository> _contentBlockRepository = null!;
    private Mock<IMapper> _mapper = null!;
    private RemoveContentBlockFromPageTemplateCommandHandler _removeContentBlockFromPageTemplateCommandHandler = null!;
    private ContentBlock _defaultContentBlock = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _pageTemplateRepository = new Mock<IPageTemplateRepository>();
        _contentBlockRepository = new Mock<IContentBlockRepository>();
        _mapper = new Mock<IMapper>();
        _removeContentBlockFromPageTemplateCommandHandler = new RemoveContentBlockFromPageTemplateCommandHandler(
            _pageTemplateRepository.Object, 
            _contentBlockRepository.Object, 
            _mapper.Object);
        
        _defaultContentBlock = new HeadlineBlock(
            1,
            DateTime.Now,
            DateTime.Now,
            false,
            0,
            0,
            1,
            1
        );
        
        DefaultPageTemplate.Blocks.Add(_defaultContentBlock);
    }

    [Test]
    public async Task Handle_GivenValidRequest_ShouldRemoveContentBlockFromPageTemplate()
    {
        // Arrange
        _contentBlockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_defaultContentBlock);
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        _pageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);
        _mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);

        RemoveContentBlockFromPageTemplateCommand command = new(DefaultPageTemplate, _defaultContentBlock);

        // Act
        PageTemplateDto result = await _removeContentBlockFromPageTemplateCommandHandler.Handle(command, CancellationToken.None);
    
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(DefaultPageTemplate.Blocks, Does.Not.Contain(_defaultContentBlock));
        });
    }

    [Test]
    public void Handle_GivenNonExistentPageTemplate_ShouldThrowNotFoundException()
    {
        // Arrange
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultArchivedPageTemplate);

        RemoveContentBlockFromPageTemplateCommand command = new(DefaultArchivedPageTemplate, _defaultContentBlock);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _removeContentBlockFromPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Handle_GivenContentBlockNotInPageTemplate_ShouldThrowBadRequestException()
    {
        // Arrange
        ContentBlock unrelatedContentBlock = new HeadlineBlock(1, DateTime.Now, DateTime.Now, false, 0, 0, 1, 1);
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        _contentBlockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(unrelatedContentBlock);

        RemoveContentBlockFromPageTemplateCommand command = new(DefaultPageTemplate, unrelatedContentBlock);

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() =>
            _removeContentBlockFromPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const string exceptionMessage = "Repository failure";
        _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception(exceptionMessage));

        RemoveContentBlockFromPageTemplateCommand command = new(DefaultPageTemplate, _defaultContentBlock);

        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _removeContentBlockFromPageTemplateCommandHandler.Handle(command, CancellationToken.None));

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
