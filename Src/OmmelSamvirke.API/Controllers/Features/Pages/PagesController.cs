using MediatR;
using Microsoft.AspNetCore.Mvc;
using OmmelSamvirke.API.Controllers.Features.Pages.Examples;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries.ContentBlockData;
using OmmelSamvirke.Application.Features.Pages.Pages.Commands;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;
using Swashbuckle.AspNetCore.Filters;

namespace OmmelSamvirke.API.Controllers.Features.Pages;

[Route("api/[controller]")]
[ApiController]
public class PagesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PagesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Get a Page by its <paramref name="id"/>.
    /// </summary>
    /// <param name="id">The id of the Page</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PageQueryDto>> GetById(int id)
    {
        PageQueryDto page = await _mediator.Send(new GetPageQuery
        {
            PageId = id
        });

        return page;
    }

    [HttpGet("ContentBlockData/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ContentBlockDataQueryDto>>> GetContentBlockData(int id)
    {
        List<ContentBlockDataQueryDto> contentBlockData = await _mediator.Send(new GetContentBlockDataQuery
        {
            PageId = id
        });
        
        return Ok(contentBlockData);
    }
    
    [HttpGet("GetNext")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetNextPage(GetNextPageQuery getNextPageQuery)
    {
        PageQueryDto page = await _mediator.Send(getNextPageQuery);
        return Ok(page);
    }
    
    [HttpGet("GetPrevious")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetPreviousPage(GetPreviousPageQuery getPreviousPageQuery)
    {
        PageQueryDto page = await _mediator.Send(getPreviousPageQuery);
        return Ok(page);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetByCommunityId([FromQuery] GetPagesByCommunityIdQuery getPagesByCommunityIdQuery)
    {
        List<PageQueryDto> pages = await _mediator.Send(getPagesByCommunityIdQuery);
        return Ok(pages);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerRequestExample(typeof(CreatePageFromTemplateCommand), typeof(CreatePageFromTemplateCommandExample))]
    public async Task<ActionResult> Create(CreatePageFromTemplateCommand createPageFromTemplateCommand)
    {
        PageQueryDto page = await _mediator.Send(createPageFromTemplateCommand);
        return Ok(page);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerRequestExample(typeof(UpdatePageCommand), typeof(UpdatePageCommandExample))]
    public async Task<ActionResult> Update(UpdatePageCommand updatePageCommand)
    {
        PageQueryDto page = await _mediator.Send(updatePageCommand);
        return Ok(page);
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        bool isDeleted = await _mediator.Send(new DeletePageCommand
        {
            PageId = id
        });
        
        return isDeleted ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
    }
}
