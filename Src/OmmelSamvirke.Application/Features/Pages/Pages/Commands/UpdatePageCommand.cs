﻿using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands.ContentBlockData;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class UpdatePageCommand : IRequest<PageQueryDto>
{
    public PageQueryDto OriginalPage { get; init; } = new();
    public PageUpdateDto UpdatedPage { get; init; } = new();
    public List<IContentBlockDataDto> UpdatedContentBlockDataElements { get; init; } = new();
}

public class UpdatePageCommandHandler : IRequestHandler<UpdatePageCommand, PageQueryDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockDataRepositoriesAggregate _contentBlockDataRepositoriesAggregate;
    private readonly IContentBlockRepository _contentBlockRepository;

    public UpdatePageCommandHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockDataRepositoriesAggregate contentBlockDataRepositoriesAggregate,
        IContentBlockRepository contentBlockRepository
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockDataRepositoriesAggregate = contentBlockDataRepositoriesAggregate;
        _contentBlockRepository = contentBlockRepository;
    }
    
    public async Task<PageQueryDto> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
    {
        UpdatePageCommandValidator validator = new(_pageRepository, _pageTemplateRepository, _contentBlockRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page currentPage = (await _pageRepository.GetByIdAsync(request.OriginalPage.Id, cancellationToken))!;
        PageQueryDto currentPageDto = _mapper.Map<PageQueryDto>(currentPage);

        if (!currentPageDto.Equals(request.OriginalPage)||
            !AreEqualDownToMilliseconds(currentPageDto.DateModified, request.OriginalPage.DateModified)
           )
            throw new ResourceHasChangedException("The Page has changed since you last loaded it");

        List<IContentBlockDataDto> contentBlockDataDtos =
            _mapper.Map<List<IContentBlockDataDto>>(request.UpdatedContentBlockDataElements);
        List<IContentBlockData> updatedContentBlockData = GetContentBlockDataElements(contentBlockDataDtos);

        await _contentBlockDataRepositoriesAggregate.UpdateAsync(updatedContentBlockData, cancellationToken);
        
        currentPage.Name = request.UpdatedPage.Name;
        currentPage.State = request.UpdatedPage.State;
        
        Page updatedPage = await _pageRepository.UpdateAsync(currentPage, cancellationToken);
        return _mapper.Map<PageQueryDto>(updatedPage);
    }

    private List<IContentBlockData> GetContentBlockDataElements(List<IContentBlockDataDto> contentBlockDataDtos)
    {
        List<IContentBlockData> resultList = new();

        foreach (IContentBlockDataDto contentBlockDataDto in contentBlockDataDtos)
        {
            switch (contentBlockDataDto.ContentBlockType)
            {
                case ContentBlockType.HeadlineBlock:
                    resultList.Add(new HeadlineBlockData
                    {
                        Id = contentBlockDataDto.Id,
                        PageId = contentBlockDataDto.PageId,
                        ContentBlockId = contentBlockDataDto.BaseContentBlockId,
                        Headline = ((HeadlineBlockDataDto)contentBlockDataDto).Headline,
                    });
                    break;
                case ContentBlockType.ImageBlock:
                    resultList.Add(new ImageBlockData
                    {
                        Id = contentBlockDataDto.Id,
                        PageId = contentBlockDataDto.PageId,
                        ContentBlockId = contentBlockDataDto.BaseContentBlockId,
                        ImageUrl = ((ImageBlockDataDto)contentBlockDataDto).ImageUrl,
                    });
                    break;
                case ContentBlockType.PdfBlock:
                    resultList.Add(new PdfBlockData
                    {
                        Id = contentBlockDataDto.Id,
                        PageId = contentBlockDataDto.PageId,
                        ContentBlockId = contentBlockDataDto.BaseContentBlockId,
                        PdfUrl = ((PdfBlockDataDto)contentBlockDataDto).PdfUrl,
                    });
                    break;
                case ContentBlockType.SlideshowBlock:
                    resultList.Add(new SlideshowBlockData
                    {
                        Id = contentBlockDataDto.Id,
                        PageId = contentBlockDataDto.PageId,
                        ContentBlockId = contentBlockDataDto.BaseContentBlockId,
                        ImageUrls = ((SlideshowBlockDataDto)contentBlockDataDto).ImageUrls,
                    });
                    break;
                case ContentBlockType.TextBlock:
                    resultList.Add(new TextBlockData
                    {
                        Id = contentBlockDataDto.Id,
                        PageId = contentBlockDataDto.PageId,
                        ContentBlockId = contentBlockDataDto.BaseContentBlockId,
                        Text = ((TextBlockDataDto)contentBlockDataDto).Text,
                    });
                    break;
                case ContentBlockType.VideoBlock:
                    resultList.Add(new VideoBlockData
                    {
                        Id = contentBlockDataDto.Id,
                        PageId = contentBlockDataDto.PageId,
                        ContentBlockId = contentBlockDataDto.BaseContentBlockId,
                        VideoUrl = ((VideoBlockDataDto)contentBlockDataDto).VideoUrl,
                    });
                    break;
            }
        }

        return resultList;
    }
    
    private static bool AreEqualDownToMilliseconds(DateTime? date1, DateTime? date2)
    {
        if (date1 == null || date2 == null) return false;
        
        DateTime dt1 = (DateTime)date1;
        DateTime dt2 = (DateTime)date2;
            
        return dt1.Year == dt2.Year
               && dt1.Month == dt2.Month
               && dt1.Day == dt2.Day
               && dt1.Hour == dt2.Hour
               && dt1.Minute == dt2.Minute
               && dt1.Second == dt2.Second
               && dt1.Millisecond == dt2.Millisecond;
    }
}
