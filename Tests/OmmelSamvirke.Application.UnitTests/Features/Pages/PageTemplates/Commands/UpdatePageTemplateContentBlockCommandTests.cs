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

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        DefaultPageTemplate.ContentBlocks.Add(DefaultContentBlock);
        _updatePageTemplateContentBlockCommandHandler = new UpdatePageTemplateContentBlockCommandHandler(
            Mapper.Object,
            PageTemplateRepository.Object,
            ContentBlockRepository.Object,
            AdminRepository.Object
        );
    }

    [Test]
    public async Task Handle_GivenValidRequest_ShouldUpdatePageTemplateContentBlock()
    {
        // Arrange
        ContentBlockDto oldContentBlock = new(
            DefaultContentBlockDto.Id,
            true,
            DefaultContentBlockLayoutConfigurationDto,
            DefaultContentBlockLayoutConfigurationDto,
            DefaultContentBlockLayoutConfigurationDto,
            DefaultContentBlockDto.ContentBlockType
        );
        DefaultPageTemplateDto.ContentBlocks.Add(oldContentBlock);
        UpdatePageTemplateContentBlockCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto, 1);

        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);
        ContentBlockRepository.Setup(repo => repo.UpdateAsync(It.IsAny<ContentBlock>())).ReturnsAsync(DefaultContentBlock);
        ContentBlockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultContentBlock);
        AdminRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultAdmin);
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
        UpdatePageTemplateContentBlockCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto, 1);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _updatePageTemplateContentBlockCommandHandler.Handle(command, CancellationToken.None)
        );
    }

    [Test]
    public void Handle_GivenNonExistentContentBlock_ShouldThrowResourceNotFoundException()
    {
        // Arrange
        ContentBlockRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((ContentBlock)null!);
        UpdatePageTemplateContentBlockCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto, 1);

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
        UpdatePageTemplateContentBlockCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto, 1);
        
        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() => _updatePageTemplateContentBlockCommandHandler.Handle(command, CancellationToken.None));

        // Assert
        Assert.That(ex?.Message, Is.EqualTo(exceptionMessage));
    }
}
