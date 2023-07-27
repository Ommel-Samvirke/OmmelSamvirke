using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetContentBlockDataQueryValidator : AbstractValidator<GetContentBlockQuery>
{
    public GetContentBlockDataQueryValidator(ILayoutConfigurationRepository layoutConfigurationRepository)
    {
        RuleFor(p => p.LayoutConfigurationId)
            .MustAsync(layoutConfigurationRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("LayoutConfiguration does not exist");
    }
}
