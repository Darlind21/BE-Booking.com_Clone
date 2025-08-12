using BookingClone.API.Extensions;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Features.Booking.Commands.ApproveBooking;
using BookingClone.Application.Features.Booking.Commands.CancelBooking;
using BookingClone.Application.Features.Booking.Commands.CreateBooking;
using BookingClone.Application.Features.Booking.Commands.RejectBooking;
using BookingClone.Application.Features.Booking.Queries.GetBookingDetails;
using BookingClone.Application.Features.Booking.Queries.GetBookingsForUser;
using BookingClone.Domain.Enums;
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
            createBookingDTO = createBookingDTO with { UserId = User.GetUserId() };
            var command = new CreateBookingCommand { CreateBookingDTO = createBookingDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }


        [Authorize]
        [HttpGet("details/{bookingId}")]
        public async Task<IActionResult> GetBookingDetails([FromRoute] Guid bookingId)
        {
            var query = new GetBookingDetailsQuery { BookingId = bookingId };

            var result = await _sender.Send(query);

            return result.ToIActionResult();
        }


        [Authorize]
        [HttpGet("my-bookings")]
        public async Task<IActionResult> GetBookingsForUser([FromQuery] BookingSearchParams searchParams)
        {
            searchParams = searchParams with { UserId = User.GetUserId() }; //forcibly making it so the user can only see his bookings only
            var query = new GetBookingsForUserQuery { BookingSearchParams = searchParams };

            var result = await _sender.Send(query);

            Response.AddPaginationHeader(result.ValueOrDefault);

            return result.ToIActionResult();
        }


        [Authorize]
        [HttpPatch("cancel/{bookingId}")]
        public async Task<IActionResult> CancelBooking([FromRoute] Guid bookingId)
        {
            var command = new CancelBookingCommand
            {
                BookingId = bookingId,
                UserId = User.GetUserId()
            };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }


        [Authorize(Roles = nameof(AppRole.Owner))]
        [HttpPatch("confirm/{bookingId}")]
        public async Task<IActionResult> ConfirmBooking([FromRoute] Guid bookingId)
        {
            var command = new ConfirmBookingCommand
            {
                BookingId = bookingId,
                UserId = User.GetUserId()
            };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }


        [Authorize(Roles = nameof(AppRole.Owner))]
        [HttpPatch("reject/{bookingId}")]
        public async Task<IActionResult> RejectBooking([FromRoute] Guid bookingId)
        {
            var command = new RejectBookingCommand
            {
                BookingId = bookingId,
                UserId = User.GetUserId()
            };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }
    }
}
 