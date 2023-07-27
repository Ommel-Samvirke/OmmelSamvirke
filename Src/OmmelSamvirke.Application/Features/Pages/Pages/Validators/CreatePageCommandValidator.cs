using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class CreatePageCommandValidator : AbstractValidator<CreatePageCommand>
{
    public CreatePageCommandValidator(
        ICommunityRepository communityRepository,
        IPageRepository pageRepository
    )
    {
        RuleFor(p => p.Page.Name)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name is required")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name cannot be longer than 200 characters")
            .MustAsync((p, cancellationToken) => pageRepository.IsPropertyUniqueAsync("Name", p, cancellationToken))
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Name must be unique");

        RuleFor(p => p.Page.CommunityId)
            .MustAsync(communityRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Community does not exist");

        RuleFor(p => p.Page.State)
            .IsInEnum()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("State must be a valid state");

        RuleFor(p => p.Page.DesktopConfiguration)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Desktop configuration is required");
        
        RuleFor(p => p.Page.TabletConfiguration)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Tablet configuration is required");
        
        RuleFor(p => p.Page.MobileConfiguration)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Mobile configuration is required");
    }
}
