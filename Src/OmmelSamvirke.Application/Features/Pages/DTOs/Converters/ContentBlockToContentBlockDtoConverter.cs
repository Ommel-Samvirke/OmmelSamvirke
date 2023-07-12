using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Converters;

[UsedImplicitly]
public class ContentBlockToContentBlockDtoConverter : ITypeConverter<ContentBlock, ContentBlockDto>
{
    public ContentBlockDto Convert(ContentBlock source, ContentBlockDto destination, ResolutionContext context)
    {
        return context.Mapper.Map<ContentBlockDto>(source);
    }
}
