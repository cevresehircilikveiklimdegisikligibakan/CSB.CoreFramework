using CSB.Core.Entities.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CSB.Core.WebAPI.Controllers
{
    public abstract class CSBControllerBase : ControllerBase
    {
        public IActionResult ToActionResult<T>(ServiceResponse<T> serviceResponse) where T : class
        {
            if (serviceResponse.IsSuccess)
            {
                if (serviceResponse.Data != null)
                    return Ok(serviceResponse.Data);
                else
                    return NoContent();
            }
            else
            {
                return NotFound(serviceResponse.Message);
            }
        }
    }
}