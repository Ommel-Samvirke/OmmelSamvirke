using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Admins.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class UpdatePageNameCommandValidator : AbstractValidator<UpdatePageNameCommand>
{
    private readonly IPageRepository _pageRepository;
    private readonly IAdminRepository _adminRepository;

    public UpdatePageNameCommandValidator(IPageRepository pageRepository, IAdminRepository adminRepository)
    {
        _pageRepository = pageRepository;
        _adminRepository = adminRepository;
        RuleFor(x => x.PageId)
            .MustAsync(PageMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
        
        RuleFor(x => x.PageName)
            .NotNull()
            .WithMessage("Page name must be set")
            .Must(x => x.Length > 3)
            .WithMessage("Page name must be longer than 3 characters");
        
        RuleFor(x => x.AdminId)
            .NotNull()
            .WithMessage("Admin id must be set")
            .MustAsync(AdminMustExist)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Admin does not exist");
    }
    
    private async Task<bool> PageMustExist(int pageId, CancellationToken cancellationToken)
    {
        return await _pageRepository.GetByIdAsync(pageId) is not null;
    }
    
    private async Task<bool> AdminMustExist(int adminId, CancellationToken cancellationToken)
    {
        return await _adminRepository.GetByIdAsync(adminId) is not null;
    }
}
