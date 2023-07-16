using AutoMapper;
using JetBrains.Annotations;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.DTOs.Queries.Converters;

[UsedImplicitly]
public class ContentBlockToContentBlockQueryDtoConverter : ITypeConverter<ContentBlock, ContentBlockQueryDto>
{
    public ContentBlockQueryDto Convert(ContentBlock source, ContentBlockQueryDto destination, ResolutionContext context)
    {
        return context.Mapper.Map<ContentBlockQueryDto>(source);
    }
}
