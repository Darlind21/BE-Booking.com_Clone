using BookingClone.API.Extensions;
using BookingClone.Application.Features.Booking.Commands.CreateBooking;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Controllers
{
    public class BookingsController(ISender _sender) : BaseAPIController
    {
        [Authorize]
        [HttpPost("book")]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO createBookingDTO)
        {
            var command = new CreateBookingCommand { CreateBookingDTO = createBookingDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }
    }
}
 