using MediatR;
using Microsoft.AspNetCore.Mvc;
using OmmelSamvirke.Application.Features.Pages.DTOs.Queries;
using OmmelSamvirke.Application.Features.Pages.Pages.Queries;

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
}
