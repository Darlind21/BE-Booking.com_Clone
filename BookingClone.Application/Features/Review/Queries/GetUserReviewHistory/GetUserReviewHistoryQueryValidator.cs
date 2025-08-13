using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Review.Queries.GetUserReviewHistory
{
    public class GetUserReviewHistoryQueryValidator : AbstractValidator<GetUserReviewHistoryQuery>
    {
        public GetUserReviewHistoryQueryValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.ReviewSearchParams)
                .NotNull().WithMessage("Search parameters are required.");

            RuleFor(x => x.ReviewSearchParams.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

            RuleFor(x => x.ReviewSearchParams.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0.")
                .LessThanOrEqualTo(1000).WithMessage("PageSize cannot exceed 1000.");

            RuleFor(x => x.ReviewSearchParams.MinRating)
                .InclusiveBetween((byte)0, (byte)10)
                .When(x => x.ReviewSearchParams.MinRating.HasValue)
                .WithMessage("MinRating must be between 0 and 10.");

            RuleFor(x => x.ReviewSearchParams.MaxRating)
                .InclusiveBetween((byte)0, (byte)10)
                .When(x => x.ReviewSearchParams.MaxRating.HasValue)
                .WithMessage("MaxRating must be between 0 and 10.");

            RuleFor(x => x.ReviewSearchParams)
                .Must(p => p.MaxRating == 0 || p.MinRating <= p.MaxRating)
                .When(x => x.ReviewSearchParams.MaxRating.HasValue && x.ReviewSearchParams.MinRating.HasValue)
                .WithMessage("MinRating cannot be greater than MaxRating.");

            RuleFor(x => x.ReviewSearchParams)
                .Must(p => !(p.FromDate.HasValue && p.ToDate.HasValue && p.FromDate > p.ToDate))
                .WithMessage("FromDate cannot be after ToDate.");
        }
    }
}
