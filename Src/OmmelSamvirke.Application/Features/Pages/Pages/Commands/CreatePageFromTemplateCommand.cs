using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class CreatePageFromTemplateCommand : IRequest<PageDto>
{
    public PageTemplateDto PageTemplateDto { get; }
    public string PageName { get; }

    public CreatePageFromTemplateCommand(PageTemplateDto pageTemplateDto, String pageName)
    {
        PageTemplateDto = pageTemplateDto;
        PageName = pageName;
    }
}

public class CreatePageFromTemplateCommandHandler : IRequestHandler<CreatePageFromTemplateCommand, PageDto>
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
    
    public async Task<PageDto> Handle(CreatePageFromTemplateCommand request, CancellationToken cancellationToken)
    {
        CreatePageFromTemplateCommandValidator validator = new(_pageTemplateRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplateDto.Id))!;
        Page page = new(request.PageName, pageTemplate);
        Page createdPage = await _pageRepository.CreateAsync(page);

        List<IContentBlockData> contentBlockDataElements = CreateContentBlockDataElements(pageTemplate, createdPage);
        await _contentBlockDataRepository.CreateAsync(contentBlockDataElements);

        return _mapper.Map<PageDto>(createdPage);
    }

    private static List<IContentBlockData> CreateContentBlockDataElements(PageTemplate pageTemplate, Page createdPage)
    {
        List<IContentBlockData> contentBlockData = new();
        foreach (ContentBlock contentBlock in pageTemplate.ContentBlocks)
        {
            switch (contentBlock)
            {
                case HeadlineBlock hb:
                    contentBlockData.Add(new HeadlineBlockData(hb, "Overskrift", (int)createdPage.Id!));
                    break;
                case ImageBlock ib:
                    contentBlockData.Add(new ImageBlockData(ib, "https://fakeimg.pl/600x400?text=Billede",
                        (int)createdPage.Id!));
                    break;
                case PdfBlock pb:
                    contentBlockData.Add(new PdfBlockData(pb, "https://fakeimg.pl/600x400?text=PDF", (int)createdPage.Id!));
                    break;
                case SlideshowBlock sb:
                    contentBlockData.Add(new SlideshowBlockData(sb, new List<string>
                    {
                        "https://fakeimg.pl/600x400?text=Billede1",
                        "https://fakeimg.pl/600x400?text=Billede2",
                        "https://fakeimg.pl/600x400?text=Billede3"
                    }, (int)createdPage.Id!));
                    break;
                case TextBlock tb:
                    contentBlockData.Add(new TextBlockData(tb, "Tekstindhold", (int)createdPage.Id!));
                    break;
                case VideoBlock vb:
                    contentBlockData.Add(new VideoBlockData(vb, "https://www.youtube.com/watch?v=c21QZnQtGqo",
                        (int)createdPage.Id!));
                    break;
            }
        }

        return contentBlockData;
    }
}
