using Microsoft.AspNetCore.Mvc;

namespace SGHT.API.Utils
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// This method automatically returns the proper response type based on code.
        /// </summary>
        /// <param name="result">Takes OperationResult</param>
        protected IActionResult HandleResponse(dynamic result)
        {
            return result.Code switch
            {
                200 => Ok(result.Data),
                201 => Created("", result.Data),
                400 => BadRequest(result.Message),
                401 => Unauthorized(result.Message),
                403 => Forbid(),
                404 => NotFound(result.Message),
                500 => StatusCode(500, result.Message),
                _ => StatusCode(result.Code, result.Message) // Fallback
            };
        }

        protected bool IsIdValid(int id)
        {
            return id > 0;
        }
    }

}
