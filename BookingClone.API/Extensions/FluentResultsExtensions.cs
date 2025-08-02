using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Extensions
{
    public static class FluentResultsExtensions
    {
        public static IActionResult ToIActionResult<T> (this Result<T> result)
        {
            if (result.IsSuccess)
                return result.Value == null ? new OkResult() : new OkObjectResult(result.Value); //OKResult() has no body ... OkObjectResult() has body but it still can be null
            //Check if we are returning value to avoid client confusion or wasted bandwidth 
            else
            {
                return new BadRequestObjectResult(new
                {
                    Errors = result.Errors.Select(x => x.Message)
                });
                //we do not do return BadRequestObjectResult(result.Errors) because to make it more client-friendly
                //the raw result.Errors - contains complex internal objs 
                //                      - may include metadata, stack traces or internal reasons
                //                      -is often not serialized cleanly/clearly in JSON
            }
        }
    }
}
