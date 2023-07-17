using MediatR;
using Microsoft.AspNetCore.Mvc;
using OmmelSamvirke.API.Controllers.Features.Pages.Examples;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Commands;
using OmmelSamvirke.Application.Features.Pages.PageTemplates.Queries;
using OmmelSamvirke.Domain.Features.Pages.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace OmmelSamvirke.API.Controllers.Features.Pages;

[Route("api/[controller]")]
[ApiController]
public class PageTemplatesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PageTemplatesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Get a Page Template by its <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The id of the Page Template</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageTemplateQueryDto>> GetById(int id)
    {
        PageTemplateQueryDto pageTemplate = await _mediator.Send(new GetPageTemplateQuery
        {
            PageTemplateId = id
        });

        return Ok(pageTemplate);
    }
    
    /// <summary>
    /// Retrieves a list of Page Templates by their <paramref name="state"/>.
    /// </summary>
    /// <param name="state">The state of the Page Templates that should be returned </param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<PageTemplateWithoutContentBlocksQueryDto>>> GetByState([FromQuery]PageTemplateState state)
    {
        List<PageTemplateWithoutContentBlocksQueryDto> pageTemplates = 
            await _mediator.Send(new GetPageTemplatesByStateQuery
            {
                PageTemplateState = state
            });

        return Ok(pageTemplates);
    }
    
    /// <summary>
    /// Create a new Page Template.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(CreatePageTemplateCommand), typeof(CreatePageTemplateCommandExample))]
    public async Task<ActionResult> Create(CreatePageTemplateCommand createPageTemplateCommand)
    {
        createPageTemplateCommand.PageTemplateCreateDto.State = PageTemplateState.Hidden;
        PageTemplateQueryDto createdPageTemplate = await _mediator.Send(createPageTemplateCommand);
        return CreatedAtAction(nameof(Create), createdPageTemplate);
    }
    
    /// <summary>
    /// Create a PageTemplate from an existing Page.
    /// </summary>
    /// <param name="createPageTemplateFromPageCommand"></param>
    /// <returns></returns>
    [HttpPost("FromPage")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateFromPage(CreatePageTemplateFromPageCommand createPageTemplateFromPageCommand)
    {
        PageTemplateQueryDto createdPageTemplate = await _mediator.Send(createPageTemplateFromPageCommand);
        return CreatedAtAction(nameof(CreateFromPage), createdPageTemplate);
    }
    
    /// <summary>
    /// Change the state of a Page Template to Public.
    /// </summary>
    /// <param name="id">The id of the Page Template that should be made public</param>
    [HttpPut("MakePublic/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> MakePublic(int id)
    {
        PageTemplateQueryDto updatedPageTemplate = await _mediator.Send(new MakePageTemplatePublicCommand
        {
            PageTemplateId = id
        });
        return Ok(updatedPageTemplate);
    }
    
    /// <summary>
    /// Change the state of a Page Template to Archived.
    /// </summary>
    /// <param name="id">The id of the Page Template that should be archived</param>
    [HttpPut("Archive/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Archive(int id)
    {
        PageTemplateQueryDto updatedPageTemplate = await _mediator.Send(new ArchivePageTemplateCommand
        {
            PageTemplateId = id
        });
        return Ok(updatedPageTemplate);
    }
    
    /// <summary>
    /// Update the state of a Page Template.
    /// </summary>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(UpdatePageTemplateCommand), typeof(UpdatePageTemplateCommandExample))]
    public async Task<ActionResult> Update(UpdatePageTemplateCommand updatePageTemplateCommand)
    {
        PageTemplateQueryDto updatedPageTemplate = await _mediator.Send(updatePageTemplateCommand);
        return Ok(updatedPageTemplate);
    }
    
    /// <summary>
    /// Delete a Page Template.
    /// </summary>
    /// <param name="id">The id of the Page Template that should be deleted</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Delete(int id)
    {
        bool isDeleted = await _mediator.Send(new DeletePageTemplateCommand
        {
            PageTemplateId = id
        });

        return isDeleted ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
    }
}
