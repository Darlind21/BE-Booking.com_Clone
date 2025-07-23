using BookingClone.Application.Features.User.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Controllers
{
    public class AccountController(ISender _sender) : BaseAPIController
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO dto)
        {
            var command = new RegisterUserCommand { RegisterUserDTO = dto };

            var result = await _sender.Send(command);

            if (result == Guid.Empty) return BadRequest();

            return Ok(result);
        }
    }
}
