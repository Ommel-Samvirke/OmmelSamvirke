using AutoMapper;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs.Converters;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs.ValueResolvers;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.MappingProfiles;

public class PageTemplateProfile : Profile
{
    public PageTemplateProfile()
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
    }
}
