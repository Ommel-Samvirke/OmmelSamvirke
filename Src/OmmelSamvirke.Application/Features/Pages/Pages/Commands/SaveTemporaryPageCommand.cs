using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class SaveTemporaryPageCommand : IRequest<PageDto>
{
    public PageDto OriginalPage { get; }
    public PageDto UpdatedPage { get; }

    public SaveTemporaryPageCommand(PageDto originalPage, PageDto updatedPage)
    {
        OriginalPage = originalPage;
        UpdatedPage = updatedPage;
    }
}

public class SaveTemporaryPageCommandHandler : IRequestHandler<SaveTemporaryPageCommand, PageDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;

    public SaveTemporaryPageCommandHandler(IMapper mapper, IPageRepository pageRepository)
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
    }
    
    public async Task<PageDto> Handle(SaveTemporaryPageCommand request, CancellationToken cancellationToken)
    {
        SaveTemporaryPageCommandValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page currentPage = (await _pageRepository.GetByIdAsync(request.OriginalPage.Id))!;
        PageDto currentPageDto = _mapper.Map<PageDto>(currentPage);

        if (!currentPageDto.Equals(request.OriginalPage))
            throw new ResourceHasChangedException("The Page has changed since you last loaded it");

        Page tempPage = await _pageRepository.GetTempByIdAsync(request.UpdatedPage.Id);
        Page updatedPage = await _pageRepository.UpdateAsync(tempPage);
        return _mapper.Map<PageDto>(updatedPage);
    }
}
