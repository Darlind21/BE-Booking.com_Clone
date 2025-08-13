using BookingClone.API.Extensions;
using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Features.Booking.Queries.GetBookingsForApartment;
using BookingClone.Application.Features.Review.Commands.SubmitReview;
using BookingClone.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Controllers
{
    public class ReviewsController (ISender _sender): BaseAPIController
    {
        [Authorize]
        [HttpPost("submit-review")]
        public async Task<IActionResult> SubmitReview([FromBody] ReviewDTO reviewDTO)
        {
            var command = new SubmitReviewCommand
            {
                ReviewDTO = reviewDTO,
                UserId = User.GetUserId()
            };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }
    }
}
