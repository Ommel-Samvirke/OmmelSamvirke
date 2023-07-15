using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

public class SaveTemporaryPageTemplateCommandTests : PageTemplateCommandsTestBase
{
    private SaveTemporaryPageTemplateCommandHandler _saveTemporaryPageTemplateCommandHandler = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        _saveTemporaryPageTemplateCommandHandler = new SaveTemporaryPageTemplateCommandHandler(
            Mapper.Object,
            PageTemplateRepository.Object,
            ContentBlockRepository.Object
        );
    }
    
    [Test]
    public async Task Handle_GivenValidRequest_ShouldSaveTemporaryPageTemplate()
    {
        // Arrange
        Mapper.Setup(m => m.Map<PageTemplate>(It.IsAny<PageTemplateDto>())).Returns(DefaultPageTemplate);
        Mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);

        UpdatePageTemplateCommand command = new(DefaultPageTemplateDto, DefaultPageTemplateDto);

        // Act
        PageTemplateDto result = await _saveTemporaryPageTemplateCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(DefaultPageTemplateDto));
    }
    
    [Test]
    public void Handle_GivenNonExistentPageTemplate_ShouldThrowValidationException()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PageTemplate)null!);
        UpdatePageTemplateCommand command = new(DefaultPageTemplateDto, DefaultPageTemplateDto);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() => _saveTemporaryPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Handle_GivenOriginalPageTemplateHasChanged_ShouldThrowResourceHasChangedException()
    {
        // Arrange
        Mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultArchivedPageTemplateDto);
        Mapper.Setup(m => m.Map<PageTemplate>(It.IsAny<PageTemplateDto>())).Returns(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        UpdatePageTemplateCommand command = new(DefaultPageTemplateDto, DefaultPageTemplateDto);

        // Act / Assert
        Assert.ThrowsAsync<ResourceHasChangedException>(() => _saveTemporaryPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }
}
