using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Review.Commands.SubmitReview
{
    public class SubmitReviewCommandHandler 
        (IBookingRepository bookingRepository, IReviewRepository reviewRepository)
        : IRequestHandler<SubmitReviewCommand, Result<ReviewDTO>>
    {
        public async Task<Result<ReviewDTO>> Handle(SubmitReviewCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ReviewDTO;

            var booking = await bookingRepository.GetByIdAsync(dto.BookingId);
            if (booking == null) throw new Exception("Booking with this id does not exist");

            var bookingDoneByUser = await bookingRepository.IsBookingDoneByUser(request.UserId, dto.BookingId);
            if (!bookingDoneByUser) throw new Exception("This booking is not done by this user");

            var existingReview = await reviewRepository.ExistsAsync(r => r.BookingId == dto.BookingId && r.UserId == request.UserId);
            if (existingReview) return Result.Fail("User has already submitted a review for this booking.");

            if (booking.Status != BookingStatus.Completed)
                return Result.Fail("Only completed bookings can be reviewed");

            if (booking.EndDate > DateOnly.FromDateTime(DateTime.UtcNow))
                return Result.Fail("Cannot submit a review before the booking is completed");

            // users can submit review only within 30 days of booking enddate
            if (DateOnly.FromDateTime(DateTime.UtcNow) > booking.EndDate.AddDays(30))
                return Result.Fail("Review period has expired");

            //for low ratings a comment is requrir
            if (dto.RatingOutOfTen < 5 && string.IsNullOrWhiteSpace(dto.Comment))
                return Result.Fail("Please provide a comment for low ratings");



            var review = new Domain.Entities.Review(dto.RatingOutOfTen, dto.BookingId, request.UserId, dto.Comment);

            var created = await reviewRepository.AddAsync(review);
            if (!created) throw new Exception("Unable to submit a review at this time");

            return new ReviewDTO
            {
                RatingOutOfTen = review.RatingOutOfTen,
                Comment = review.Comment,
                BookingId = review.BookingId,
                ReviewId = review.Id
            };
        }
    }
}
