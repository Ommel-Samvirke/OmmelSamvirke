using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class CreatePageTemplateCommandTests : PageTemplateCommandsTestBase
{
    private CreatePageTemplateCommandHandler _createPageTemplateCommandHandler = null!;
    private CreatePageTemplateCommand _defaultCreatePageTemplateCommand = null!;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _createPageTemplateCommandHandler = new CreatePageTemplateCommandHandler(Mapper.Object, PageTemplateRepository.Object);
        _defaultCreatePageTemplateCommand = new CreatePageTemplateCommand(
            "Test",
            new List<ContentBlockDto>() { DefaultContentBlockDto },
            PageTemplateState.Public
        );
    }
    
    [Test]
    public async Task Handle_WhenCalledWithValidRequest_ShouldCreatePageTemplate()
    {
        Mapper.Setup(m => m.Map<PageTemplate>(It.IsAny<CreatePageTemplateCommand>())).Returns(DefaultPageTemplate);
        Mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);

        // Act
        PageTemplateDto result = await _createPageTemplateCommandHandler.Handle(_defaultCreatePageTemplateCommand, CancellationToken.None);

        // Assert
        result.ShouldBe(DefaultPageTemplateDto);
        PageTemplateRepository.Verify(r => r.CreateAsync(DefaultPageTemplate), Times.Once);
    }
    
    [TestCase(0)]
    [TestCase(2)]
    [TestCase(51)]
    [TestCase(1000)]
    public void Handle_WhenCalledWithTooShortName_ShouldThrowBadRequestException(int nameLength)
    {
        // Arrange
        CreatePageTemplateCommand createPageTemplateCommand = new(
            new string('a', nameLength),
            new List<ContentBlockDto>() { DefaultContentBlockDto },
            PageTemplateState.Public
        );

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() =>
            _createPageTemplateCommandHandler.Handle(createPageTemplateCommand, CancellationToken.None)
        );
    }

    [Test]
    public void Handle_WhenCalledWithEmptyContentBlocks_ShouldThrowBadRequestException()
    {
        // Arrange
        CreatePageTemplateCommand createPageTemplateCommand = new(
            "TestTemplate",
            new List<ContentBlockDto>(),
            PageTemplateState.Public
        );

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() =>
            _createPageTemplateCommandHandler.Handle(createPageTemplateCommand, CancellationToken.None)
        );
    }
    
    [Test]
    public void Handle_WhenCalledWithInvalidPageTemplateStatus_ShouldThrowBadRequestException()
    {
        // Arrange
        CreatePageTemplateCommand createPageTemplateCommand = new(
            "TestTemplate",
            new List<ContentBlockDto>() { DefaultContentBlockDto },
            (PageTemplateState)9999
        );

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() =>
            _createPageTemplateCommandHandler.Handle(createPageTemplateCommand, CancellationToken.None)
        );
    }
    
    [Test]
    public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
    {
        // Arrange
        const string exceptionMessage = "Repository failure";

        CreatePageTemplateCommand createPageTemplateCommand = new(
            "Test",
            new List<ContentBlockDto>() { DefaultContentBlockDto },
            PageTemplateState.Public
        );
        
        Mapper.Setup(m => m.Map<PageTemplate>(It.IsAny<CreatePageTemplateCommand>())).Returns(DefaultPageTemplate);
        PageTemplateRepository.Setup(r => r.CreateAsync(It.IsAny<PageTemplate>()))
            .ThrowsAsync(new Exception(exceptionMessage));

        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _createPageTemplateCommandHandler.Handle(createPageTemplateCommand, CancellationToken.None)
        );

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
