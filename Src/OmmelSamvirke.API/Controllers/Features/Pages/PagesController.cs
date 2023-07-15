using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}