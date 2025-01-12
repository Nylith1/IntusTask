using IntusTask.Domain.Handlers.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IntusTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RectangleController(IMediator mediator) : BaseController
{
    private const string JsonFilePath = "rectangle.json";

    [HttpGet(nameof(GetRectangle))]
    public async Task<IActionResult> GetRectangle()
    {
        var result = await mediator.Send(new GetRectangleRequest());
        return MapToActionResult(result);
    }

    [HttpPatch(nameof(UpdateRectangle))]
    public async Task<IActionResult> UpdateRectangle([FromBody] UpdateRectangleRequest request, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(request, cancellationToken);
        return MapToActionResult(result);
    }
}