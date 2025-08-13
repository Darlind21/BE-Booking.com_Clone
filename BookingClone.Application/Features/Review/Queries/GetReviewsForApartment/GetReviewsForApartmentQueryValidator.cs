using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Review.Queries.GetReviewsForApartment
{
    public class GetReviewsForApartmentQueryValidator : AbstractValidator<GetReviewsForApartmentQuery>
    {
        public GetReviewsForApartmentQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.ReviewsSearchParams)
                .NotNull().WithMessage("Search parameters are required.");

            RuleFor(x => x.ReviewsSearchParams.ApartmentId)
                .NotEmpty().WithMessage("ApartmentId is required.");

            RuleFor(x => x.ReviewsSearchParams.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

            RuleFor(x => x.ReviewsSearchParams.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
                .LessThanOrEqualTo(1000).WithMessage("PageSize cannot exceed 1000.");

            RuleFor(x => x.ReviewsSearchParams.MinRating)
                .InclusiveBetween((byte)0, (byte)10)
                .WithMessage("MinRating must be between 0 and 10.");

            RuleFor(x => x.ReviewsSearchParams.MaxRating)
                .InclusiveBetween((byte)0, (byte)10)
                .WithMessage("MaxRating must be between 0 and 10.");

            RuleFor(x => x.ReviewsSearchParams)
                .Must(p => p.MaxRating == 0 || p.MinRating <= p.MaxRating)
                .WithMessage("MinRating cannot be greater than MaxRating.");

            RuleFor(x => x.ReviewsSearchParams)
                .Must(p => !(p.FromDate.HasValue && p.ToDate.HasValue && p.FromDate > p.ToDate))
                .WithMessage("FromDate cannot be after ToDate.");
        }
    }
}
