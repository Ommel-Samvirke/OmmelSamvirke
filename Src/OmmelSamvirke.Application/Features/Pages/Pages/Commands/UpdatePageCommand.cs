using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class UpdatePageCommand : IRequest<PageDto>
{
    public PageDto OriginalPage { get; init;  } = null!;
    public PageDto UpdatedPage { get; init; } = null!;
}

// Todo: Should be updated to be more efficient and safe by not deleting all layout configurations
public class UpdatePageCommandHandler : IRequestHandler<UpdatePageCommand, PageDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly ICommunityRepository _communityRepository;
    private readonly ILayoutConfigurationRepository _layoutConfigurationRepository;

    public UpdatePageCommandHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        ICommunityRepository communityRepository,
        ILayoutConfigurationRepository layoutConfigurationRepository
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _communityRepository = communityRepository;
        _layoutConfigurationRepository = layoutConfigurationRepository;
    }
    
    public async Task<PageDto> Handle(UpdatePageCommand request, CancellationToken cancellationToken)
    {
        UpdatePageCommandValidator validator = new(_pageRepository, _communityRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page currentPage = (await _pageRepository.GetByIdWithRelationsAsync(request.OriginalPage.Id, cancellationToken))!;
        PageDto currentPageDto = _mapper.Map<PageDto>(currentPage);

        if (!currentPageDto.Equals(request.OriginalPage))
        {
            throw new ResourceHasChangedException("The Page has changed since you last loaded it");
        }

        await _layoutConfigurationRepository.DeleteByPageAsync(currentPage, cancellationToken);

        currentPage.Name = request.UpdatedPage.Name;
        currentPage.State = request.UpdatedPage.State;
        currentPage.DesktopConfiguration = _mapper.Map<LayoutConfiguration>(request.UpdatedPage.DesktopConfiguration);
        currentPage.TabletConfiguration = _mapper.Map<LayoutConfiguration>(request.UpdatedPage.TabletConfiguration);
        currentPage.MobileConfiguration = _mapper.Map<LayoutConfiguration>(request.UpdatedPage.MobileConfiguration);
        
        Page updatedPage = await _pageRepository.UpdateAsync(currentPage, cancellationToken);
        return _mapper.Map<PageDto>(updatedPage);
    }
}
