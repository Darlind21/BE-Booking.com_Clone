using BookingClone.API.Extensions;
using BookingClone.Application.Features.Owner.Commands.RegisterOwner;
using BookingClone.Application.Features.User.Commands.DeleteUserProfile;
using BookingClone.Application.Features.User.Commands.LoginUser;
using BookingClone.Application.Features.User.Commands.RegisterUser;
using BookingClone.Application.Features.User.Commands.UpdateUserProfile;
using BookingClone.Application.Features.User.Queries.GetUserProfile;
using BookingClone.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

            return result.ToIActionResult();
        }


        [HttpPost("owner/register")]
        public async Task<IActionResult> RegisterOwner([FromBody] RegisterOwnerDTO registerOwnerDTO)
        {
            var command = new RegisterOwnerCommand { RegisterOwnerDTO = registerOwnerDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var command = new LoginUserCommand { LoginUserDTO = loginUserDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }


        [Authorize]
        [HttpGet("test-authorization")]
        public IActionResult Test()
        {
            return Ok();
        }


        [Authorize(Roles = nameof(AppRole.Owner))]
        [HttpGet("test-owner-authorization")]
        public IActionResult TestOwner()
        {
            return Ok();
        }


        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var userId = User.GetUserId();
            var query = new GetUserProfileQuery { UserId = userId };

            var result = await _sender.Send(query);

            return result.ToIActionResult();
        }


        [Authorize]
        [HttpPut("profile/edit")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserProfileDTO updateUserProfileDTO)
        {
            var command = new UpdateUserProfileCommand { UpdateUserProfileDTO = updateUserProfileDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }


        //[Authorize]
        //[HttpDelete("profile/delete")]
        //public async Task<IActionResult> DeleteUserProfile()
        //{
        //    var userId = User.GetUserId();
        //    var command = new DeleteUserProfileCommand { UserId = userId };

        //    var result = await _sender.Send(command);
        //    return result.ToIActionResult();
        //}

    }
}
