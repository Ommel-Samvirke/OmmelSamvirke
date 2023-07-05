using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands;

[TestFixture]
public class CreatePageTemplateFromPageCommandTests : PageTemplateCommandsTestBase
{
    private CreatePageTemplateFromPageCommand _defaultCommand = null!;
    private CreatePageTemplateFromPageCommandHandler _defaultHandler = null!;
    
    [SetUp]
    public override void SetUp()
    {
        base.SetUp();
        _defaultCommand = new CreatePageTemplateFromPageCommand(DefaultPageDto); 
        _defaultHandler = new CreatePageTemplateFromPageCommandHandler(
            Mapper.Object,
            PageRepository.Object,
            PageTemplateRepository.Object,
            ContentBlockDataRepository.Object
        );
    }
    
    [Test]
    public void Handle_PageDoesNotExist_ThrowsNotFoundException()
    {
        Page page = new("TestName", DefaultPageTemplate);
        
        // Arrange
        PageRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(page);
        ContentBlockDataRepository.Setup(x => 
            x.GetByPageIdAsync(It.IsAny<int>())).ReturnsAsync(new List<IContentBlockData>());

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(async () => 
            await _defaultHandler.Handle(_defaultCommand, CancellationToken.None));
    }

    [Test]
    public void Handle_PageHasNoContentBlocks_ThrowsNotFoundException()
    {
        // Arrange
        PageRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPage);
        ContentBlockDataRepository.Setup(x => 
            x.GetByPageIdAsync(It.IsAny<int>())).ReturnsAsync(new List<IContentBlockData>());

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(async () => 
            await _defaultHandler.Handle(_defaultCommand, CancellationToken.None));
    }

    [Test]
    public async Task Handle_PageIsValid_CreatesPageTemplate()
    {
        // Arrange
        HeadlineBlockData testContentBlockData = new(DefaultContentBlock, "TestHeadline", 1);
        PageTemplate newPage = new(
            "CustomPageTemplate",
            new List<ContentBlock>(),
            PageTemplateState.Public
        );
        
        PageRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPage);
        PageTemplateRepository.Setup(x => x.CreateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(newPage);
        Mapper.Setup(x => x.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);
        
        ContentBlockDataRepository.Setup(x => x.GetByPageIdAsync(It.IsAny<int>())).ReturnsAsync(
            new List<IContentBlockData> { testContentBlockData }
        );
        
        // Act
        PageTemplateDto result = await _defaultHandler.Handle(_defaultCommand, CancellationToken.None);
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(DefaultPageTemplateDto, Is.EqualTo(result));
            Assert.That(DefaultPage.Template, Is.EqualTo(newPage));
        });
        PageTemplateRepository.Verify(x => x.CreateAsync(It.IsAny<PageTemplate>()), Times.Once);
    }
}
