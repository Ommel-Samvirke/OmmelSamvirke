using FluentValidation;
using OmmelSamvirke.Application.Errors;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Domain.Features.Pages.Interfaces.Repositories;
using OmmelSamvirke.Domain.Features.Pages.Models;

namespace OmmelSamvirke.Application.Features.Pages.PageTemplates.Validators;

public class DeletePageTemplateCommandValidator : AbstractValidator<DeletePageTemplateCommand>
{
    private readonly IPageTemplateRepository _pageTemplateRepository;
    private readonly IPageRepository _pageRepository;

    public DeletePageTemplateCommandValidator(IPageTemplateRepository pageTemplateRepository, IPageRepository pageRepository)
    {
        
    }
    
}
