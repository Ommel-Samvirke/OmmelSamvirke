using MediatR;
using Microsoft.AspNetCore.Mvc;
using OmmelSamvirke.API.Controllers.Features.Pages.Examples;
using OmmelSamvirke.Application.Features.Pages.DTOs;
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
    public async Task<ActionResult<PageTemplateQueryDto>> Get(int id)
    {
        PageTemplateQueryDto pageTemplate = await _mediator.Send(new GetPageTemplateQuery(id));

        return pageTemplate;
    }
    
    
    /// <summary>
    /// Retrieves a list of Page Templates by their <paramref name="state"/>.
    /// </summary>
    /// <param name="state">The state of the Page Templates that should be returned </param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<PageTemplateWithoutContentBlocksQueryDto>>> Get([FromQuery]PageTemplateState state)
    {
        List<PageTemplateWithoutContentBlocksQueryDto> pageTemplates = 
            await _mediator.Send(new GetPageTemplatesByStateQuery(state));

        return pageTemplates;
    }
    
    /// <summary>
    /// Create a new Page Template.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerRequestExample(typeof(CreatePageTemplateCommand), typeof(CreatePageTemplateCommandExample))]
    public async Task<ActionResult> Post(CreatePageTemplateCommand createPageTemplateCommand)
    {
        createPageTemplateCommand.PageTemplateCreateDto.State = PageTemplateState.Hidden;
        PageTemplateQueryDto pageTemplate = await _mediator.Send(createPageTemplateCommand);
        return CreatedAtAction(nameof(Post), pageTemplate);
    }
    
    /// <summary>
    /// Change the state of a Page Template to Public.
    /// </summary>
    /// <param name="id">The id of the Page Template that should be made public</param>
    /// <returns></returns>
    [HttpPut("make-public/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Put(int id)
    {
        PageTemplateQueryDto pageTemplate = await _mediator.Send(new MakePageTemplatePublicCommand(id));
        return Ok(pageTemplate);
    }
}
