using AutoMapper;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlockData;
using OmmelSamvirke.Application.Features.Pages.DTOs.Converters;
using OmmelSamvirke.Application.Features.Pages.DTOs.ValueResolvers;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.MappingProfiles;

public class PagesProfile : Profile
{
    public PagesProfile()
    {
        CreateMap<PageTemplate, PageTemplateDto>()
            .ForMember(dest => dest.ContentBlocks, 
                opt => opt.MapFrom<ContentBlockToDtoResolver>())
            .ReverseMap()
            .ForMember(dest => dest.ContentBlocks, 
                opt => opt.MapFrom<DtoToContentBlockResolver>());
        CreateMap<ContentBlock, ContentBlockDto>().ConvertUsing<ContentBlockToContentBlockDtoConverter>();
        CreateMap<ContentBlockDto, ContentBlock>().ConvertUsing<ContentBlockDtoToContentBlockConverter>();
        CreateMap<ContentBlockDto, HeadlineBlock>().ReverseMap();
        CreateMap<ContentBlockDto, ImageBlock>().ReverseMap();
        CreateMap<ContentBlockDto, PdfBlock>().ReverseMap();
        CreateMap<ContentBlockDto, SlideshowBlock>().ReverseMap();
        CreateMap<ContentBlockDto, TextBlock>().ReverseMap();
        CreateMap<ContentBlockDto, VideoBlock>().ReverseMap();
        CreateMap<ContentBlockLayoutConfiguration, ContentBlockLayoutConfigurationDto>().ReverseMap();
        CreateMap<CreatePageTemplateCommand, PageTemplate>();
        CreateMap<PageTemplate, PageTemplateWithoutContentBlocksDto>();
        CreateMap<HeadlineBlockData, HeadlineBlockDataDto>();
        CreateMap<ImageBlockData, ImageBlockDataDto>();
        CreateMap<PdfBlockData, PdfBlockDataDto>();
        CreateMap<SlideshowBlockData, SlideshowBlockDataDto>();
        CreateMap<TextBlockData, TextBlockDataDto>();
        CreateMap<VideoBlockData, VideoBlockDataDto>();
    }
}
