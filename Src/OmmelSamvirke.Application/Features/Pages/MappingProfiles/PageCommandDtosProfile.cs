using AutoMapper;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands.ContentBlockData;
using OmmelSamvirke.Application.Features.Pages.DTOs.Commands.Converters;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.MappingProfiles;

public class PageCommandDtosProfile : Profile
{
    public PageCommandDtosProfile()
    {
        CreateMap<PageTemplateCreateDto, PageTemplate>();
        CreateMap<PageTemplateUpdateDto, PageTemplate>();

        CreateMap<ContentBlockCreateDto, ContentBlockQueryDto>().ReverseMap();
        CreateMap<ContentBlock, ContentBlockCreateDto>();
        CreateMap<ContentBlockCreateDto, ContentBlock>().ConvertUsing<ContentBlockCreateDtoToContentBlockConverter>();
        CreateMap<ContentBlockCreateDto, HeadlineBlock>();
        CreateMap<ContentBlockCreateDto, ImageBlock>();
        CreateMap<ContentBlockCreateDto, PdfBlock>();
        CreateMap<ContentBlockCreateDto, SlideshowBlock>();
        CreateMap<ContentBlockCreateDto, TextBlock>();
        CreateMap<ContentBlockCreateDto, VideoBlock>();
        
        CreateMap<ContentBlockLayoutConfigurationCreateDto, ContentBlockLayoutConfiguration>();
        CreateMap<ContentBlockLayoutConfigurationQueryDto, ContentBlockLayoutConfigurationCreateDto>().ReverseMap();
        
        CreateMap<IContentBlockDataDto, IContentBlockData>()
            .ForMember(dest => dest.Page, opt => opt.Ignore())
            .ForMember(dest => dest.BaseContentBlock, opt => opt.Ignore());
        
        CreateMap<HeadlineBlockData, HeadlineBlockDataDto>()
            .ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.PageId))
            .ReverseMap();
        CreateMap<ImageBlockData, ImageBlockDataDto>()
            .ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.PageId))
            .ReverseMap();
        CreateMap<PdfBlockData, PdfBlockDataDto>()
            .ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.PageId))
            .ReverseMap();
        CreateMap<SlideshowBlockData, SlideshowBlockDataDto>()
            .ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.PageId))
            .ReverseMap();
        CreateMap<TextBlockData, TextBlockDataDto>()
            .ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.PageId))
            .ReverseMap();
        CreateMap<VideoBlockData, VideoBlockDataDto>()
            .ForMember(dest => dest.PageId, opt => opt.MapFrom(src => src.PageId))
            .ReverseMap();
    }
}
