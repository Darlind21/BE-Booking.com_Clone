using BookingClone.Application.Features.User.Commands.LoginUser;
using BookingClone.Application.Features.User.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Controllers
{
    public class AccountController(ISender _sender) : BaseAPIController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDto)
        {
            var command = new RegisterUserCommand { RegisterUserDTO = registerUserDto };

            var result = await _sender.Send(command);

            if (result.IsFailed) return BadRequest(new
            {
                Errors = result.Errors.Select(x => x.Message)
            });
            //we do not do return BadRequest(result.Errors) because to make it more client-friendly
            //the raw result.Errors - contains complex internal objs 
            //                      - may include metadata, stack traces or internal reasons
            //                      -is often not serialized cleanly/clearly in JSON

            return Ok(result.Value);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var command = new LoginUserCommand { LoginUserDTO = loginUserDTO };

            var result = await _sender.Send(command);

            if (result.IsFailed) return BadRequest(new
            {
                Errors = result.Errors.Select(x => x.Message)
            });

            return Ok(result.Value);
        }
    }
}
