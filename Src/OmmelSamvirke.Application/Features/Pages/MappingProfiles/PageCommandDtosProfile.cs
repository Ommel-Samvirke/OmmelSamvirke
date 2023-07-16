using AutoMapper;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands.Converters;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.MappingProfiles;

public class PageCommandDtosProfile : Profile
{
    public PageCommandDtosProfile()
    {
        CreateMap<PageTemplateCreateDto, PageTemplate>();
        CreateMap<PageTemplateUpdateDto, PageTemplate>();
        CreateMap<PageCreateDto, Page>();

        CreateMap<ContentBlock, ContentBlockCreateDto>();
        CreateMap<ContentBlockCreateDto, ContentBlock>().ConvertUsing<ContentBlockCreateDtoToContentBlockConverter>();
        CreateMap<ContentBlockCreateDto, HeadlineBlock>();
        CreateMap<ContentBlockCreateDto, ImageBlock>();
        CreateMap<ContentBlockCreateDto, PdfBlock>();
        CreateMap<ContentBlockCreateDto, SlideshowBlock>();
        CreateMap<ContentBlockCreateDto, TextBlock>();
        CreateMap<ContentBlockCreateDto, VideoBlock>();
        
        CreateMap<ContentBlockLayoutConfigurationCreateDto, ContentBlockLayoutConfiguration>();
    }
}
