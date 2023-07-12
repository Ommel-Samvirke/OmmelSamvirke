using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Admins.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class AddContentBlockToPageTemplateCommand : IRequest<PageTemplateDto>
{
    public ContentBlockDto ContentBlock { get; }
    public PageTemplateDto PageTemplate { get; }
    public int AdminId { get; }
    
    public AddContentBlockToPageTemplateCommand(PageTemplateDto pageTemplate, ContentBlockDto contentBlock, int adminId)
    {
        PageTemplate = pageTemplate;
        ContentBlock = contentBlock;
        AdminId = adminId;
    }
}

public class AddContentBlockToPageTemplateCommandHandler : IRequestHandler<AddContentBlockToPageTemplateCommand, PageTemplateDto>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IMapper _mapper;

    public AddContentBlockToPageTemplateCommandHandler(
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository,
        IAdminRepository adminRepository,
        IMapper mapper
    )
    {
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;
        _adminRepository = adminRepository;
        _mapper = mapper;
    }
    public async Task<PageTemplateDto> Handle(AddContentBlockToPageTemplateCommand request, CancellationToken cancellationToken)
    {
        AddContentBlockToPageTemplateCommandValidator validator = new(_pageTemplateRepository, _adminRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        ContentBlock contentBlock = await _contentBlockRepository.CreateAsync(_mapper.Map<ContentBlock>(request.ContentBlock));
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplate.Id))!;
        pageTemplate.ContentBlocks.Add(contentBlock);
        
        PageTemplate updatedPageTemplate = await _pageTemplateRepository.TempUpdateAsync(pageTemplate, request.AdminId);
        return _mapper.Map<PageTemplateDto>(updatedPageTemplate);
    }
}
