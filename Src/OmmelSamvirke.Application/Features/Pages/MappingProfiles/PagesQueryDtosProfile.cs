using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries.Converters;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ValueResolvers;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlockData;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.MappingProfiles;

public class PagesQueryDtosProfile : Profile
{
    public PagesQueryDtosProfile()
    {
        CreateMap<PageTemplate, PageTemplateQueryDto>()
            .ForMember(dest => dest.ContentBlocks, 
                opt => opt.MapFrom<ContentBlockToDtoResolver>())
            .ReverseMap()
            .ForMember(dest => dest.ContentBlocks, 
                opt => opt.MapFrom<DtoToContentBlockResolver>());
        CreateMap<ContentBlock, ContentBlockQueryDto>().ConvertUsing<ContentBlockToContentBlockDtoConverter>();
        CreateMap<ContentBlockQueryDto, ContentBlock>().ConvertUsing<ContentBlockDtoToContentBlockConverter>();
        CreateMap<ContentBlockQueryDto, HeadlineBlock>().ReverseMap();
        CreateMap<ContentBlockQueryDto, ImageBlock>().ReverseMap();
        CreateMap<ContentBlockQueryDto, PdfBlock>().ReverseMap();
        CreateMap<ContentBlockQueryDto, SlideshowBlock>().ReverseMap();
        CreateMap<ContentBlockQueryDto, TextBlock>().ReverseMap();
        CreateMap<ContentBlockQueryDto, VideoBlock>().ReverseMap();
        CreateMap<ContentBlockLayoutConfiguration, ContentBlockLayoutConfigurationQueryDto>().ReverseMap();
        CreateMap<PageTemplateWithoutContentBlocksQueryDto, PageTemplate>().ReverseMap();
        CreateMap<HeadlineBlockData, HeadlineBlockDataQueryDto>();
        CreateMap<ImageBlockData, ImageBlockDataQueryDto>();
        CreateMap<PdfBlockData, PdfBlockDataQueryDto>();
        CreateMap<SlideshowBlockData, SlideshowBlockDataQueryDto>();
        CreateMap<TextBlockData, TextBlockDataQueryDto>();
        CreateMap<VideoBlockData, VideoBlockDataQueryDto>();
        CreateMap<Page, PageQueryDto>().ReverseMap();
    }
}
