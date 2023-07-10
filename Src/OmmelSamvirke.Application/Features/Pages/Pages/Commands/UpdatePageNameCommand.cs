using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class UpdatePageNameCommand : IRequest<PageDto>
{
    public int PageId { get; }
    public string PageName { get; }

    public UpdatePageNameCommand(int pageId, String pageName)
    {
        PageId = pageId;
        PageName = pageName;
    }   
}

public class UpdatePageNameCommandHandler : IRequestHandler<UpdatePageNameCommand, PageDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;

    public UpdatePageNameCommandHandler(
        IMapper mapper,
        IPageRepository pageRepository
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
    }
    
    public async Task<PageDto> Handle(UpdatePageNameCommand request, CancellationToken cancellationToken)
    {
        UpdatePageNameCommandValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page page = (await _pageRepository.GetByIdAsync(request.PageId))!;
        page.Name = request.PageName;
        
        Page updatedPage = await _pageRepository.UpdateAsync(page);
        return _mapper.Map<PageDto>(updatedPage);
    }
}
