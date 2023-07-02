using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class CreatePageTemplateCommandTests
{
    private Mock<IMapper> _mapper = null!;
    private Mock<IPageTemplateRepository> _pageTemplateRepository = null!;
    private CreatePageTemplateCommandHandler _createPageTemplateCommandHandler = null!;
    private CreatePageTemplateCommand _defaultCreatePageTemplateCommand = null!;
    
    [SetUp]
    public void SetUp()
    {
        _mapper = new Mock<IMapper>();
        _pageTemplateRepository = new Mock<IPageTemplateRepository>();
        _createPageTemplateCommandHandler = new CreatePageTemplateCommandHandler(_mapper.Object, _pageTemplateRepository.Object);
        _defaultCreatePageTemplateCommand = new CreatePageTemplateCommand(
            "Test",
            new HashSet<Layouts>() { Layouts.Desktop },
            new List<ContentBlock>() { new TextBlock(false, 0, 0, 100, null) },
            PageTemplateState.Public
        );
    }
    
    [Test]
    public async Task Handle_WhenCalledWithValidRequest_ShouldCreatePageTemplate()
    {
        PageTemplate pageTemplate = new("TestTemplate", new HashSet<Layouts>(), new List<ContentBlock>(), PageTemplateState.Public);
        _mapper.Setup(m => m.Map<PageTemplate>(It.IsAny<CreatePageTemplateCommand>())).Returns(pageTemplate);

        PageTemplateDto pageTemplateDto = new(
            1, 
            "TestTemplate", 
            new HashSet<Layouts>(), 
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        
        _mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(pageTemplateDto);

        // Act
        PageTemplateDto result = await _createPageTemplateCommandHandler.Handle(_defaultCreatePageTemplateCommand, CancellationToken.None);

        // Assert
        result.ShouldBe(pageTemplateDto);
        _pageTemplateRepository.Verify(r => r.CreateAsync(pageTemplate), Times.Once);
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
            new HashSet<Layouts>() { Layouts.Desktop },
            new List<ContentBlock>() { new TextBlock(false, 0, 0, 100, null) },
            PageTemplateState.Public
        );

        // Act / Assert
        Assert.ThrowsAsync<BadRequestException>(() =>
            _createPageTemplateCommandHandler.Handle(createPageTemplateCommand, CancellationToken.None)
        );
    }

    [Test]
    public void Handle_WhenCalledWithEmptySupportedLayouts_ShouldThrowBadRequestException()
    {
        // Arrange
        CreatePageTemplateCommand createPageTemplateCommand = new(
            "TestTemplate",
            new HashSet<Layouts>(),
            new List<ContentBlock>() { new TextBlock(false, 0, 0, 100, null) },
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
            new HashSet<Layouts>() { Layouts.Desktop },
            new List<ContentBlock>(),
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
            new HashSet<Layouts>() { Layouts.Desktop },
            new List<ContentBlock>() { new TextBlock(false, 0, 0, 100, null) },
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
            new HashSet<Layouts>() { Layouts.Desktop },
            new List<ContentBlock>() { new TextBlock(false, 0, 0, 100, null) },
            PageTemplateState.Public
        );

        PageTemplate pageTemplate = new("TestTemplate", new HashSet<Layouts>(), new List<ContentBlock>(), PageTemplateState.Public);
        _mapper.Setup(m => m.Map<PageTemplate>(It.IsAny<CreatePageTemplateCommand>())).Returns(pageTemplate);

        _pageTemplateRepository.Setup(r => r.CreateAsync(It.IsAny<PageTemplate>()))
            .ThrowsAsync(new Exception(exceptionMessage));

        // Act
        Exception? ex = Assert.ThrowsAsync<Exception>(() =>
            _createPageTemplateCommandHandler.Handle(createPageTemplateCommand, CancellationToken.None)
        );

        // Assert
        ex?.Message.ShouldBe(exceptionMessage);
    }
}
