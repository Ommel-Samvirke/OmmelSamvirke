using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Admins.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class AddContentBlockToPageTemplateCommandValidator : AbstractValidator<AddContentBlockToPageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IAdminRepository _adminRepository;

    public AddContentBlockToPageTemplateCommandValidator(
        IPageTemplateRepository pageTemplateRepository,
        IAdminRepository adminRepository
    )
    {
        _pageTemplateRepository = pageTemplateRepository;
        _adminRepository = adminRepository;
        RuleFor(x => x.ContentBlock)
            .NotNull()
            .WithMessage("Content block cannot be null.");

        RuleFor(x => x.PageTemplate)
            .NotNull()
            .WithMessage("Page template cannot be null.")
            .MustAsync(PageTemplateMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist.");
        
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
    
    private async Task<bool> AdminMustExist(int adminId, CancellationToken cancellationToken)
    {
        return await _adminRepository.GetByIdAsync(adminId) is not null;
    }
}
