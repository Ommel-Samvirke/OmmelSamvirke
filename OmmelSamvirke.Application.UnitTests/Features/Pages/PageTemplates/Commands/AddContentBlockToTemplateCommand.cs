using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands.Common;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using AddContentBlockToTemplateCommand = OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands.AddContentBlockToTemplateCommand;

namespace OmmelSamvirke.Application.UnitTests.Features.Pages.PageTemplates.Commands
{
    [TestFixture]
    public class AddContentBlockToTemplateCommandHandlerTests : PageTemplateCommandsTestBase
    {
        private Mock<IPageTemplateRepository> _pageTemplateRepository = null!;
        private Mock<IContentBlockRepository> _contentBlockRepository = null!;
        private Mock<IMapper> _mapper = null!;
        private AddContentBlockToTemplateCommandHandler _addContentBlockToTemplateCommandHandler = null!;
        
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _pageTemplateRepository = new Mock<IPageTemplateRepository>();
            _contentBlockRepository = new Mock<IContentBlockRepository>();
            _mapper = new Mock<IMapper>();
            _addContentBlockToTemplateCommandHandler = new AddContentBlockToTemplateCommandHandler(
                _pageTemplateRepository.Object, 
                _contentBlockRepository.Object, 
                _mapper.Object);
        }

        [Test]
        public async Task Handle_GivenValidRequest_ShouldAddContentBlockToPageTemplate()
        {
            // Arrange
            ContentBlock contentBlock = new HeadlineBlock(false, 0, 0, 1, 1);
            _contentBlockRepository.Setup(repo => repo.CreateAsync(contentBlock)).ReturnsAsync(contentBlock);
            _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(DefaultPageTemplate);
            _pageTemplateRepository.Setup(repo => repo.UpdateAsync(It.IsAny<PageTemplate>())).ReturnsAsync(DefaultPageTemplate);
            _mapper.Setup(m => m.Map<PageTemplateDto>(It.IsAny<PageTemplate>())).Returns(DefaultPageTemplateDto);

            AddContentBlockToTemplateCommand command = new(DefaultPageTemplate, contentBlock);

            // Act
            PageTemplateDto result = await _addContentBlockToTemplateCommandHandler.Handle(command, CancellationToken.None);
            
            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(DefaultPageTemplate.Blocks, Does.Contain(contentBlock));
            });
        }

        [Test]
        public void Handle_GivenInvalidRequest_ShouldThrowNotFoundException()
        {
            // Arrange
            AddContentBlockToTemplateCommand command = new(DefaultPageTemplate, null!);

            // Act / Assert
            Assert.ThrowsAsync<NotFoundException>(() =>
                _addContentBlockToTemplateCommandHandler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_GivenNonExistentPageTemplate_ShouldThrowNotFoundException()
        {
            // Arrange
            ContentBlock contentBlock = new HeadlineBlock(false, 0, 0, 1, 1);
            _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((PageTemplate)null!);

            AddContentBlockToTemplateCommand command = new(DefaultPageTemplate, contentBlock);

            // Act / Assert
            Assert.ThrowsAsync<NotFoundException>(() =>
                _addContentBlockToTemplateCommandHandler.Handle(command, CancellationToken.None));
        }

        [Test]
        public void Handle_WhenRepositoryThrowsException_ShouldRethrowException()
        {
            // Arrange
            const string exceptionMessage = "Repository failure";
            ContentBlock contentBlock = new HeadlineBlock(false, 0, 0, 1, 1);
            _pageTemplateRepository.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception(exceptionMessage));

            AddContentBlockToTemplateCommand command = new(DefaultPageTemplate, contentBlock);

            // Act
            Exception? ex = Assert.ThrowsAsync<Exception>(() =>
                _addContentBlockToTemplateCommandHandler.Handle(command, CancellationToken.None));

            // Assert
            ex?.Message.ShouldBe(exceptionMessage);
        }
    }
}
