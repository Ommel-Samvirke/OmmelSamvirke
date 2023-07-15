using FluentValidation;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class GetPageQueryValidator : AbstractValidator<GetPageQuery>
{
    private readonly IPageRepository _pageRepository;

    public GetPageQueryValidator(IPageRepository pageRepository)
    {
        
    }
    
}
