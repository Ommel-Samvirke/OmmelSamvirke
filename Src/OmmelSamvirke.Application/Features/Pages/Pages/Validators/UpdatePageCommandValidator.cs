using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;

namespace OmmelSamvirke.Application.Features.Pages.Pages.Validators;

public class UpdatePageCommandValidator : AbstractValidator<UpdatePageCommand>
{
    private readonly IPageRepository _pageRepository;

    public UpdatePageCommandValidator(IPageRepository pageRepository)
    {
       
    }
}
