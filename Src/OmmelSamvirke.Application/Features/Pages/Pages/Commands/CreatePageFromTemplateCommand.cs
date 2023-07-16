using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;
using OmmelSamvirke.Domain.ValueObjects;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class CreatePageFromTemplateCommand : IRequest<PageQueryDto>
{
    public int PageTemplateId { get; set; }
    public string PageName { get; set; } = string.Empty;
}

public class CreatePageFromTemplateCommandHandler : IRequestHandler<CreatePageFromTemplateCommand, PageQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IPageRepository _pageRepository;
    private readonly IContentBlockDataRepository _contentBlockDataRepository;

    public CreatePageFromTemplateCommandHandler(
        IMapper mapper,
        IPageTemplateRepository pageTemplateRepository,
        IPageRepository pageRepository,
        IContentBlockDataRepository contentBlockDataRepository
    )
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
        _pageRepository = pageRepository;
        _contentBlockDataRepository = contentBlockDataRepository;
    }
    
    public async Task<PageQueryDto> Handle(CreatePageFromTemplateCommand request, CancellationToken cancellationToken)
    {
        CreatePageFromTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplateId))!;
        Page page = new()
        {
            Name = request.PageName, 
            Template = pageTemplate
        };
        Page createdPage = await _pageRepository.CreateAsync(page);

        List<IContentBlockData> contentBlockDataElements = CreateContentBlockDataElements(pageTemplate, createdPage);
        await _contentBlockDataRepository.CreateAsync(contentBlockDataElements);

        return _mapper.Map<PageQueryDto>(createdPage);
    }

    private static List<IContentBlockData> CreateContentBlockDataElements(PageTemplate pageTemplate, Page createdPage)
    {
        List<IContentBlockData> contentBlockData = new();
        foreach (ContentBlock contentBlock in pageTemplate.ContentBlocks)
        {
            switch (contentBlock)
            {
                case HeadlineBlock hb:
                    contentBlockData.Add(new HeadlineBlockData
                    {
                        ContentBlock = hb,
                        Headline = "Overskrift",
                        Page = createdPage
                    });
                    break;
                case ImageBlock ib:
                    contentBlockData.Add(new ImageBlockData
                    {
                        ContentBlock = ib,
                        ImageUrl = "https://fakeimg.pl/600x400?text=Billede",
                        Page = createdPage
                    });
                    break;
                case PdfBlock pb:
                    contentBlockData.Add(new PdfBlockData
                    {
                        ContentBlock = pb,
                        PdfUrl = "https://fakeimg.pl/600x400?text=PDF",
                        Page = createdPage
                    });
                    break;
                case SlideshowBlock sb:
                    contentBlockData.Add(new SlideshowBlockData
                    {
                        ContentBlock = sb,
                        ImageUrls = new List<Url>
                        {
                            new("https://fakeimg.pl/600x400?text=Billede1"),
                            new("https://fakeimg.pl/600x400?text=Billede2"),
                            new("https://fakeimg.pl/600x400?text=Billede3")
                        },
                        Page = createdPage
                    });
                    break;
                case TextBlock tb:
                    contentBlockData.Add(new TextBlockData
                    {
                        ContentBlock = tb,
                        Text = "Tekstindhold",
                        Page = createdPage
                    });
                    break;
                case VideoBlock vb:
                    contentBlockData.Add(new VideoBlockData
                    {
                        ContentBlock = vb, 
                        VideoUrl = "https://www.youtube.com/watch?v=c21QZnQtGqo",
                        Page = createdPage
                    });
                    break;
            }
        }

        return contentBlockData;
    }
}
