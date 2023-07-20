using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Communities.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class CreatePageFromTemplateCommandValidator : AbstractValidator<CreatePageFromTemplateCommand>
{
    public CreatePageFromTemplateCommandValidator(
        IPageTemplateRepository pageTemplateRepository,
        ICommunityRepository communityRepository,
        IPageRepository pageRepository
    )
    {
        RuleFor(p => p.PageTemplateId)
            .MustAsync(pageTemplateRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page template does not exist");
        
        RuleFor(p => p.PageName)
            .NotEmpty()
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name is required")
            .MaximumLength(200)
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Name cannot be longer than 200 characters")
            .MustAsync((p, cancellationToken) => pageRepository.IsPropertyUniqueAsync("Name", p, cancellationToken))
            .WithErrorCode(ErrorCode.BadRequest)
            .WithMessage("Page Name must be unique");
        
        RuleFor(p => p.CommunityId)
            .MustAsync(communityRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Community does not exist");
    }
}
