using AutoMapper;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.MappingProfiles;

public class PageCommandDtosProfile : Profile
{
    public PageCommandDtosProfile()
    {
        CreateMap<PageTemplateCreateDto, PageTemplate>();
        CreateMap<PageCreateDto, Page>();
        CreateMap<ContentBlockCreateDto, ContentBlock>();
        CreateMap<ContentBlockCreateDto, HeadlineBlock>();
        CreateMap<ContentBlockCreateDto, ImageBlock>();
        CreateMap<ContentBlockCreateDto, PdfBlock>();
        CreateMap<ContentBlockCreateDto, SlideshowBlock>();
        CreateMap<ContentBlockCreateDto, TextBlock>();
        CreateMap<ContentBlockCreateDto, VideoBlock>();
    }
}
