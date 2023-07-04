using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Domain.UnitTests.Features.Pages.Models;

public abstract class PagesBaseTestModel
{
    protected ContentBlockLayoutConfiguration DefaultDesktopConfiguration { get; set; }
    protected ContentBlockLayoutConfiguration DefaultMobileConfiguration { get; set; }
    protected ContentBlockLayoutConfiguration DefaultTabletConfiguration { get; set; }
    
    protected PdfBlock DefaultPdfBlock { get; set; }

    protected SlideshowBlock DefaultSlideshowBlock { get; set; }

    protected TextBlock DefaultTextBlock { get; set; }

    protected VideoBlock DefaultVideoBlock { get; set; }

    protected ImageBlock DefaultImageBlock { get; set; }

    protected HeadlineBlock DefaultHeadlineBlock { get; set; }
    
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
        
        DefaultHeadlineBlock = new HeadlineBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration
        );
        
        DefaultImageBlock = new ImageBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration
        );
        
        DefaultPdfBlock = new PdfBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration
        );
        
        DefaultSlideshowBlock = new SlideshowBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration
        );
        
        DefaultTextBlock = new TextBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration
        );
        
        DefaultVideoBlock = new VideoBlock(
            false,
            DefaultDesktopConfiguration,
            DefaultTabletConfiguration,
            DefaultMobileConfiguration
        );
    }
}
