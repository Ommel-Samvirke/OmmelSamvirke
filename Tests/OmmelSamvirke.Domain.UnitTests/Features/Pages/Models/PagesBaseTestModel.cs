using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models;

public abstract class PagesBaseTestModel
{
    protected ContentBlockLayoutConfiguration DefaultDesktopConfiguration { get; private set; } = null!;
    protected ContentBlockLayoutConfiguration DefaultMobileConfiguration { get; private set; } = null!;
    protected ContentBlockLayoutConfiguration DefaultTabletConfiguration { get; private set; } = null!;

    protected PdfBlock DefaultPdfBlock { get; private set; } = null!;

    protected SlideshowBlock DefaultSlideshowBlock { get; private set; } = null!;

    protected TextBlock DefaultTextBlock { get; private set; } = null!;

    protected VideoBlock DefaultVideoBlock { get; private set; } = null!;

    protected ImageBlock DefaultImageBlock { get; private set; } = null!;

    protected HeadlineBlock DefaultHeadlineBlock { get; private set; } = null!;
    
    protected PageTemplate DefaultPageTemplate { get; private set; } = null!;
    protected Page DefaultPage { get; private set; } = null!;

    [SetUp]
    public virtual void SetUp()
    {
        DateTime now = DateTime.Now;
        DefaultDesktopConfiguration = new ContentBlockLayoutConfiguration(
            1, 
            now,
            now,
            0,
            0,
            6,
            10
        );
        DefaultTabletConfiguration = new ContentBlockLayoutConfiguration(
            2,
            now,
            now,
            50,
            15,
            12,
            2
        );
        DefaultMobileConfiguration = new ContentBlockLayoutConfiguration(
            3,
            now,
            now,
            10,
            8,
            3,
            5
        );

        DefaultPageTemplate = new PageTemplate(
            1,
            now,
            now,
            "TestTemplate",
            new List<ContentBlock>(),
            PageTemplateState.Public
        );

        DefaultPage = new Page(1, now, now, "TestPage", DefaultPageTemplate);
        
        DefaultHeadlineBlock = new HeadlineBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration,
            DefaultPageTemplate
        );
        
        DefaultImageBlock = new ImageBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration,
            DefaultPageTemplate
        );
        
        DefaultPdfBlock = new PdfBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration,
            DefaultPageTemplate
        );
        
        DefaultSlideshowBlock = new SlideshowBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration,
            DefaultPageTemplate
        );
        
        DefaultTextBlock = new TextBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration,
            DefaultPageTemplate
        );
        
        DefaultVideoBlock = new VideoBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration,
            DefaultPageTemplate
        );
    }
}
