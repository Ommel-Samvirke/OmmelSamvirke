using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetContentBlockDataQueryValidator : AbstractValidator<GetContentBlockDataQuery>
{
    public GetContentBlockDataQueryValidator(IPageRepository pageRepository)
    {
        RuleFor(p => p.PageId)
            .MustAsync(pageRepository.ExistsAsync)
            .WithErrorCode(ErrorCode.ResourceNotFound)
            .WithMessage("Page does not exist");
    }
}
