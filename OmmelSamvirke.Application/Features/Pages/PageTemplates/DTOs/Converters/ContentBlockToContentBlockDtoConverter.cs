using AutoMapper;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs.Converters;

public class ContentBlockToContentBlockDtoConverter : ITypeConverter<ContentBlock, ContentBlockDto>
{
    public ContentBlockDto Convert(ContentBlock source, ContentBlockDto destination, ResolutionContext context)
    {
        return context.Mapper.Map<ContentBlockDto>(source);
    }
}
