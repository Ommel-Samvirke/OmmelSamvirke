using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class ArchivePageTemplateCommandValidator : AbstractValidator<ArchivePageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;

    public ArchivePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository)
    {
   
    }

}
