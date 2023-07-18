using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Enums;
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
    public int CommunityId { get; set; }
}

public class CreatePageFromTemplateCommandHandler : IRequestHandler<CreatePageFromTemplateCommand, PageQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IPageRepository _pageRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly IContentBlockDataRepositoriesAggregate _contentBlockDataRepositoriesAggregate;

    public CreatePageFromTemplateCommandHandler(
        IMapper mapper,
        IPageTemplateRepository pageTemplateRepository,
        IPageRepository pageRepository,
        ICommunityRepository communityRepository,
        IContentBlockDataRepositoriesAggregate contentBlockDataRepositoriesAggregate
    )
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
        _pageRepository = pageRepository;
        _communityRepository = communityRepository;
        _contentBlockDataRepositoriesAggregate = contentBlockDataRepositoriesAggregate;
    }
    
    public async Task<PageQueryDto> Handle(CreatePageFromTemplateCommand request, CancellationToken cancellationToken)
    {
        CreatePageFromTemplateCommandValidator validator = new(_pageTemplateRepository, _communityRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page page = new()
        {
            Name = request.PageName, 
            TemplateId = request.PageTemplateId,
            CommunityId = request.CommunityId,
            State = PageState.Hidden
        };
        Page createdPage = await _pageRepository.CreateAsync(page,cancellationToken);

        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsyncWithNavigationProps(request.PageTemplateId, cancellationToken))!;
        List<IContentBlockData> contentBlockDataElements = CreateContentBlockDataElements(pageTemplate, createdPage);
        await _contentBlockDataRepositoriesAggregate.CreateAsync(contentBlockDataElements, cancellationToken);

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
                        ContentBlockId = hb.Id,
                        Headline = "Overskrift",
                        PageId = createdPage.Id
                    });
                    break;
                case ImageBlock ib:
                    contentBlockData.Add(new ImageBlockData
                    {
                        ContentBlockId = ib.Id,
                        ImageUrl = "https://fakeimg.pl/600x400?text=Billede",
                        PageId = createdPage.Id
                    });
                    break;
                case PdfBlock pb:
                    contentBlockData.Add(new PdfBlockData
                    {
                        ContentBlockId = pb.Id,
                        PdfUrl = "https://fakeimg.pl/600x400?text=PDF",
                        PageId = createdPage.Id
                    });
                    break;
                case SlideshowBlock sb:
                    contentBlockData.Add(new SlideshowBlockData
                    {
                        ContentBlockId = sb.Id,
                        ImageUrls = new List<Url>
                        {
                            new("https://fakeimg.pl/600x400?text=Billede1"),
                            new("https://fakeimg.pl/600x400?text=Billede2"),
                            new("https://fakeimg.pl/600x400?text=Billede3")
                        },
                        PageId = createdPage.Id
                    });
                    break;
                case TextBlock tb:
                    contentBlockData.Add(new TextBlockData
                    {
                        ContentBlockId = tb.Id,
                        Text = "Tekstindhold",
                        PageId = createdPage.Id
                    });
                    break;
                case VideoBlock vb:
                    contentBlockData.Add(new VideoBlockData
                    {
                        ContentBlockId = vb.Id, 
                        VideoUrl = "https://www.youtube.com/watch?v=c21QZnQtGqo",
                        PageId = createdPage.Id
                    });
                    break;
            }
        }

        return contentBlockData;
    }
}
