using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
{
    public UpdatePageCommandValidator(
        IPageRepository pageRepository,
        ICommunityRepository communityRepository
    )
    {
        RuleFor(p => p.OriginalPage.Id)
            .MustAsync(pageRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");

        RuleFor(p => p.UpdatedPage.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be set")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page name must be less than 200 characters")
            .MustAsync((p, cancellationToken) => pageRepository.IsPropertyUniqueAsync("Name",p, cancellationToken))
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Name must be unique");
        
        RuleFor(p => p.UpdatedPage.CommunityId)
            .MustAsync(communityRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Community does not exist");
        
        RuleFor(p => p.UpdatedPage.State)
            .IsInEnum()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("State must be a valid state");

        RuleFor(p => p.UpdatedPage.DesktopConfiguration)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Desktop configuration is required");
        
        RuleFor(p => p.UpdatedPage.TabletConfiguration)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Tablet configuration is required");
        
        RuleFor(p => p.UpdatedPage.MobileConfiguration)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Mobile configuration is required");
    }
}
