using BookingClone.API.Extensions;
using BookingClone.Application.Features.Owner.Commands.RegisterOwner;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Controllers
{
    public class OwnerController(ISender _sender) : BaseAPIController
    {
        [HttpPost("register")] //new user registers directly as owner
        public async Task<IActionResult> RegisterOwner([FromBody] RegisterNewUserAsOwnerDTO registerNewUserAsOwnerDTO)
        {
            var command = new RegisterNewUserAsOwnerCommand { RegisterNewUserAsOwnerDTO = registerNewUserAsOwnerDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }
    }
}
