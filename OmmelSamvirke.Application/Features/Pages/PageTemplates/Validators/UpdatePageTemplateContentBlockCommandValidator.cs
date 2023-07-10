using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.DTOs;
using OmmelSamvirke.Domain.Features.Admins.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class UpdatePageTemplateContentBlockCommandValidator : AbstractValidator<UpdatePageTemplateContentBlockCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IContentBlockRepository _contentBlockRepository;
    private readonly IAdminRepository _adminRepository;

    public UpdatePageTemplateContentBlockCommandValidator(
        IPageTemplateRepository pageTemplateRepository,
        IContentBlockRepository contentBlockRepository,
        IAdminRepository adminRepository
    )
    {
        _pageTemplateRepository = pageTemplateRepository;
        _contentBlockRepository = contentBlockRepository;
        _adminRepository = adminRepository;

        RuleFor(x => x.PageTemplate)
            .NotNull()
            .WithMessage("Page template cannot be null.")
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist.");

        RuleFor(x => x.ContentBlock)
            .NotNull()
            .WithMessage("Content block cannot be null.")
            .MustAsync(ContentBlockMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Content block does not exist.")
            .Must((command, contentBlock) => command.PageTemplate.ContentBlocks.Any(cb => 
                cb.Id == contentBlock.Id &&
                cb.ContentBlockType == contentBlock.ContentBlockType)
            )
            .WithMessage("Content block must be contained in the page template.");
        
        RuleFor(x => x.AdminId)
            .NotNull()
            .WithMessage("Admin id cannot be null.")
            .GreaterThan(0)
            .WithMessage("Admin id must be greater than 0.")
            .MustAsync(AdminMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Admin does not exist.");
    }
    
    private async Task<bool> PageTemplateMustExist(PageTemplateDto pageTemplate, CancellationToken cancellationToken)
    {
        return await _pageTemplateRepository.GetByIdAsync(pageTemplate.Id) is not null;
    }
    
    private async Task<bool> ContentBlockMustExist(ContentBlockDto contentBlock, CancellationToken cancellationToken)
    {
        return await _contentBlockRepository.GetByIdAsync(contentBlock.Id) is not null;
    }
    
    private async Task<bool> AdminMustExist(int adminId, CancellationToken cancellationToken)
    {
        return await _adminRepository.GetByIdAsync(adminId) is not null;
    }
}
