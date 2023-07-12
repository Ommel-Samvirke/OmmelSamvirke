using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Admins.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class UpdatePageNameCommand : IRequest<PageDto>
{
    public int PageId { get; }
    public string PageName { get; }
    public int AdminId { get; }

    public UpdatePageNameCommand(int pageId, string pageName, int adminId)
    {
        PageId = pageId;
        PageName = pageName;
        AdminId = adminId;
    }   
}

public class UpdatePageNameCommandHandler : IRequestHandler<UpdatePageNameCommand, PageDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;
    private readonly IAdminRepository _adminRepository;

    public UpdatePageNameCommandHandler(
        IMapper mapper,
        IPageRepository pageRepository,
        IAdminRepository adminRepository
    )
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
        _adminRepository = adminRepository;
    }
    
    public async Task<PageDto> Handle(UpdatePageNameCommand request, CancellationToken cancellationToken)
    {
        UpdatePageNameCommandValidator validator = new(_pageRepository, _adminRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        Page page = (await _pageRepository.GetByIdAsync(request.PageId))!;
        page.Name = request.PageName;
        
        Page updatedPage = await _pageRepository.TempUpdateAsync(page, request.AdminId);
        return _mapper.Map<PageDto>(updatedPage);
    }
}
