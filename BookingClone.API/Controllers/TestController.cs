using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Controllers
{
    public class TestController : BaseAPIController
    {
        [HttpGet("error")]
        public async Task<IActionResult> ErrorTest()
        {
            throw new Exception("Test");
        }

    }
}
