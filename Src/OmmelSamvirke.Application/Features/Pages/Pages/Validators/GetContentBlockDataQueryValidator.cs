using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetContentBlockDataQueryValidator : AbstractValidator<GetContentBlockDataQuery>
{
    private readonly IPageRepository _pageRepository;

    public GetContentBlockDataQueryValidator(IPageRepository pageRepository)
    {
        
    }
}
