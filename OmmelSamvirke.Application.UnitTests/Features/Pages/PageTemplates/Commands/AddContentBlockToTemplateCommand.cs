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
    private AddContentBlockToPageTemplateCommandHandler _addContentBlockToPageTemplateCommandHandler = null!;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        
        _addContentBlockToPageTemplateCommandHandler = new AddContentBlockToPageTemplateCommandHandler(
            PageTemplateRepository.Object,
            ContentBlockRepository.Object,
            AdminRepository.Object,
            Mapper.Object
        );
    }

    [Test]
    public async Task Handle_GivenValidRequest_ShouldAddContentBlockToPageTemplate()
    {
        // Arrange
        ContentBlockRepository.Setup(repo => repo.CreateAsync(It.IsAny<ContentBlock>())).ReturnsAsync(DefaultContentBlock);
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
        PageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);
        AdminRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultAdmin);
        Mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);

        AddContentBlockToPageTemplateCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto, 1);

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
        AddContentBlockToPageTemplateCommand command = new(DefaultPageTemplateDto, null!, 1);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _addContentBlockToPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_GivenNonExistentPageTemplate_ShouldThrowNotFoundException()
    {
        // Arrange
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PageTemplate)null!);

        AddContentBlockToPageTemplateCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto, 1);

        // Act / Assert
        Assert.ThrowsAsync<NotFoundException>(() =>
            _addContentBlockToPageTemplateCommandHandler.Handle(command, CancellationToken.None));
    }

    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const string exceptionMessage = "Repository failure";
        PageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception(exceptionMessage));

        AddContentBlockToPageTemplateCommand command = new(DefaultPageTemplateDto, DefaultContentBlockDto, 1);

        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _addContentBlockToPageTemplateCommandHandler.Handle(command, CancellationToken.None));

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
