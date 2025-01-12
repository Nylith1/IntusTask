using IntusTask.Domain.Handlers.Common;
using Microsoft.AspNetCore.Mvc;

namespace IntusTask;

public abstract class BaseController : ControllerBase
{
    protected IActionResult MapToActionResult<T>(DataResponse<T> result)
    {
        if (result.Success)
        {
            return Ok(result.Data);
        }
        else
        {
            return BadRequest(new { message = result.ErrorMessage });
        }
    }
}
