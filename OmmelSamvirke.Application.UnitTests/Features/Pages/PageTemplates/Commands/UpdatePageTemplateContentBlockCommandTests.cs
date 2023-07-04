using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class UpdatePageTemplateContentBlockCommandTests : PageTemplateCommandsTestBase
{
    private UpdatePageTemplateContentBlockCommandHandler _updatePageTemplateContentBlockCommandHandler = null!;
    private ContentBlock _defaultContentBlock = null!;
    private Mock<IContentBlockRepository> _contentBlockRepository = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _defaultContentBlock = new TextBlock(
            1,
            DateTime.Now,
            DateTime.Now,
            false,
            0,
            0,
            100,
            null
        );
        DefaultPageTemplate.Blocks.Add(_defaultContentBlock);
        _contentBlockRepository = new Mock<IContentBlockRepository>();
        _updatePageTemplateContentBlockCommandHandler = new UpdatePageTemplateContentBlockCommandHandler(Mapper.Object, PageTemplateRepository.Object, _contentBlockRepository.Object);
    }

    [Test]
    public async Task Handle_GivenValidRequest_ShouldUpdatePageTemplateContentBlock()
    {
        // Arrange
        UpdatePageTemplateContentBlockCommand command = new(DefaultPageTemplate, _defaultContentBlock);

        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);
        _contentBlockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ContentBlock>())).ReturnsAsync(_defaultContentBlock);
        _contentBlockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(_defaultContentBlock);
        Mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);

        // Act
        PageTemplateDto result = await _updatePageTemplateContentBlockCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.ShouldBe(DefaultPageTemplateDto);
    }

    [Test]
    public void Handle_GivenNonExistentPageTemplate_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PageTemplate)null!);
        UpdatePageTemplateContentBlockCommand command = new(DefaultPageTemplate, _defaultContentBlock);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _updatePageTemplateContentBlockCommandHandler.Handle(command, CancellationToken.None)
        );
    }

    [Test]
    public void Handle_GivenNonExistentContentBlock_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        _contentBlockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ContentBlock)null!);
        UpdatePageTemplateContentBlockCommand command = new(DefaultPageTemplate, _defaultContentBlock);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _updatePageTemplateContentBlockCommandHandler.Handle(command, CancellationToken.None)
        );
    }

    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const string exceptionMessage = "Repository failure";
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception(exceptionMessage));
        UpdatePageTemplateContentBlockCommand command = new(DefaultPageTemplate, _defaultContentBlock);
        
        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() => _updatePageTemplateContentBlockCommandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.That(ex?.Message, Is.EqualTo(exceptionMessage));
    }
}
