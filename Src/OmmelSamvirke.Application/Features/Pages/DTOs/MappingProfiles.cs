using AutoMapper;
using OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks;
using OmmelSamvirke.Application.Features.Pages.DTOs.ContentBlocks.Converters;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.DTOs;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ContentBlockDto, ContentBlock>().ConvertUsing<ContentBlockDtoToContentBlockConverter>();
        CreateMap<ContentBlock, ContentBlockDto>().ConvertUsing<ContentBlockToContentBlockDtoConverter>();
        
        CreateMap<HeadLineBlockDto, HeadlineBlock>().ReverseMap();
        CreateMap<ImageBlockDto, ImageBlock>().ReverseMap();
        CreateMap<PdfBlockDto, PdfBlock>().ReverseMap();
        CreateMap<SlideshowBlockDto, SlideshowBlock>().ReverseMap();
        CreateMap<TextBlockDto, TextBlock>().ReverseMap();
        CreateMap<VideoBlockDto, VideoBlock>().ReverseMap();
        
        CreateMap<LayoutConfigurationDto, LayoutConfiguration>().ReverseMap();
        CreateMap<PageDto, Page>().ReverseMap();
    }
}
