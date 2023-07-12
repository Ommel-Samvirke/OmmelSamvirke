using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

public class SavePageTemplateVersionCommandValidator : PageTemplateCommandsTestBase
{
    private SavePageTemplateVersionCommandHandler _savePageTemplateVersionCommandHandler = null!;

    [SetUp]
    public override void SetUp()
    {
        base.SetUp();

        _savePageTemplateVersionCommandHandler = new SavePageTemplateVersionCommandHandler(
            Mapper.Object,
            PageTemplateRepository.Object
        );
    }
    
    [Test]
    public async Task Handle_GivenValidRequest_ShouldSavePageTemplateVersion()
    {
        // Arrange
        Mapper.Setup(m => m.Map<PageTemplate>(It.IsAny<PageTemplateDto>())).Returns(DefaultPageTemplate);
        Mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PageTemplate?)null);
        PageTemplateRepository.Setup(repo => repo.SaveVersionAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);
        
        SavePageTemplateVersionCommand command = new(DefaultPageTemplateDto);

        // Act
        PageTemplateDto result = await _savePageTemplateVersionCommandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(DefaultPageTemplateDto));
    }
    
    [Test]
    public void Handle_GivenExistingVersion_ShouldThrowBadRequestException()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.GetVersionAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        SavePageTemplateVersionCommand command = new(DefaultPageTemplateDto);

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() => _savePageTemplateVersionCommandHandler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Validator_GivenNullPageTemplateVersionName_ShouldThrowValidationException()
    {
        // Arrange
        DefaultPageTemplateDto.Name = null!;
        SavePageTemplateVersionCommand command = new(DefaultPageTemplateDto);

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() => _savePageTemplateVersionCommandHandler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Validator_GivenNullPageTemplateVersionContentBlocks_ShouldThrowValidationException()
    {
        // Arrange
        DefaultPageTemplateDto.ContentBlocks = null!;
        SavePageTemplateVersionCommand command = new(DefaultPageTemplateDto);

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() => _savePageTemplateVersionCommandHandler.Handle(command, CancellationToken.None));
    }
}
