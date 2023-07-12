using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Exceptions;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.Pages.Validators;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Commands;

public class SavePageVersionCommand : IRequest<PageDto>
{
    public PageDto PageVersion { get; }

    public SavePageVersionCommand(PageDto pageVersion)
    {
        PageVersion = pageVersion;
    }
}

public class SavePageVersionCommandHandler : IRequestHandler<SavePageVersionCommand, PageDto>
{
    private readonly IMapper _mapper;
    private readonly IPageRepository _pageRepository;

    public SavePageVersionCommandHandler(IMapper mapper, IPageRepository pageRepository)
    {
        _mapper = mapper;
        _pageRepository = pageRepository;
    }
    
    public async Task<PageDto> Handle(SavePageVersionCommand request, CancellationToken cancellationToken)
    {
        SavePageVersionCommandValidator validator = new(_pageRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);

        bool versionExists = await _pageRepository.GetVersionAsync(request.PageVersion.Id) is not null;
        if (versionExists)
            throw new BadRequestException("Version cannot be saved, because it already exists");
        
        Page page = _mapper.Map<Page>(request.PageVersion);
        Page savedPageVersion = await _pageRepository.SaveVersionAsync(page);
        
        return _mapper.Map<PageDto>(savedPageVersion);
    }
}
