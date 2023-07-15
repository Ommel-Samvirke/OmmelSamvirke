using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.DTOs;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class CreatePageTemplateFromPageCommandValidator : AbstractValidator<CreatePageTemplateFromPageCommand>
{
    private readonly IPageRepository _pageRepository;
    private readonly IContentBlockDataRepository _contentBlockRepository;

    public CreatePageTemplateFromPageCommandValidator(
        IPageRepository pageRepository,
        IContentBlockDataRepository contentBlockRepository
    )
    {
        
    }
}
