using BookingClone.API.Extensions;
using BookingClone.Application.Features.Booking.Commands.CreateBooking;
using BookingClone.Application.Features.Booking.Queries.GetBookingDetails;
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
        public async Task<IActionResult> CreateBooking([FromBody]CreateBookingDTO createBookingDTO)
        {
            var command = new CreateBookingCommand { CreateBookingDTO = createBookingDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }

        [Authorize]
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetBookingDetails([FromRoute] Guid bookingId)
        {
            var query = new GetBookingDetailsQuery { BookingId = bookingId };

            var result = await _sender.Send(query);

            return result.ToIActionResult();
        }
    }
}
 