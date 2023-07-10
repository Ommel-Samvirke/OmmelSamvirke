using AutoMapper;
using MediatR;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;
using OmmelSamvirke.Domain.Features.Admins.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;
using OmmelSamvirke.Domain.Features.Pages.Models.ContentBlocks;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;

public class UpdatePageTemplateContentBlockCommand : IRequest<PageTemplateDto>
{
    public PageTemplateDto PageTemplate { get; }
    public ContentBlockDto ContentBlock { get; }
    public int AdminId { get; }

    public UpdatePageTemplateContentBlockCommand(PageTemplateDto pageTemplate, ContentBlockDto contentBlock, int adminId)
    {
        PageTemplate = pageTemplate;
        ContentBlock = contentBlock;
        AdminId = adminId;
    }
}

public class UpdatePageTemplateContentBlockCommandHandler : IRequestHandler<UpdatePageTemplateContentBlockCommand, PageTemplateDto>
{
    private readonly IMapper _mapper;
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;
    private readonly IAdminRepository _adminRepository;

    public UpdatePageTemplateContentBlockCommandHandler(
        IMapper mapper,
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository,
        IAdminRepository adminRepository
    )
    {
        _mapper = mapper;
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;
        _adminRepository = adminRepository;
    }
    
    public async Task<PageTemplateDto> Handle(UpdatePageTemplateContentBlockCommand request, CancellationToken cancellationToken)
    {
        UpdatePageTemplateContentBlockCommandValidator validator = new(_pageTemplateRepository, _contentBlockRepository, _adminRepository);
        ValidationResultHandler.Handle(await validator.ValidateAsync(request, cancellationToken), request);
        
        PageTemplate pageTemplate = (await _pageTemplateRepository.GetByIdAsync(request.PageTemplate.Id))!;
        ContentBlock contentBlock = (await _contentBlockRepository.GetByIdAsync(request.ContentBlock.Id))!;
        pageTemplate.ContentBlocks.Remove(pageTemplate.ContentBlocks.First(x => x.Id == contentBlock.Id));
        pageTemplate.ContentBlocks.Add(contentBlock);
        
        PageTemplate updatedPageTemplate = await _pageTemplateRepository.TempUpdateAsync(pageTemplate, request.AdminId);
        return _mapper.Map<PageTemplateDto>(updatedPageTemplate);
    }
}
