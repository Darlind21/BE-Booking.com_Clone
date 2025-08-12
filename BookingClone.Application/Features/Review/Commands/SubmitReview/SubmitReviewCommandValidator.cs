using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Review.Commands.SubmitReview
{
    public class SubmitReviewCommandValidator : AbstractValidator<SubmitReviewCommand>
    {
        public SubmitReviewCommandValidator()
        {
            RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.ReviewDTO)
                .NotNull().WithMessage("ReviewDTO is required.");

            RuleFor(x => x.ReviewDTO.RatingOutOfTen)
                    .InclusiveBetween((byte)1, (byte)10)
                    .WithMessage("Rating must be between 1 and 10.");

            RuleFor(x => x.ReviewDTO.Comment)
                .MaximumLength(1000)
                .WithMessage("Comment cannot exceed 1000 characters.");

            RuleFor(x => x.ReviewDTO.BookingId)
                .NotEmpty().WithMessage("BookingId is required.");

            When(x => x.ReviewDTO.RatingOutOfTen < 5, () =>
            {
                RuleFor(x => x.ReviewDTO.Comment)
                    .NotEmpty()
                    .WithMessage("Please provide a comment for low ratings");
            });
        }
    }
}
