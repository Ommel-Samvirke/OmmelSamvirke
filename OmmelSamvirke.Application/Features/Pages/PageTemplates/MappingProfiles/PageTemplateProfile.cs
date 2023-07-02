using AutoMapper;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.MappingProfiles;

public class PageTemplateProfile : Profile
{
    public PageTemplateProfile()
    {
        CreateMap<PageTemplateDto, PageTemplate>().ReverseMap();
        CreateMap<CreatePageTemplateCommand, PageTemplate>();
    }
}