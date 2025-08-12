using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Queries.GetBookingsForUser
{
    public class GetBookingsForUserQueryValidator : AbstractValidator<GetBookingsForUserQuery>
    {
        public GetBookingsForUserQueryValidator()
        {
            RuleFor(x => x.BookingSearchParams)
                .NotNull().WithMessage("BookingSearchParams is required.");

            RuleFor(x => x.BookingSearchParams.UserId)
                .NotNull().WithMessage("UserId must be specified.");

            RuleFor(x => x.BookingSearchParams.ApartmentId)
                .Null().WithMessage("ApartmentId must not be specified when getting bookings for user.");

            RuleFor(x => x.BookingSearchParams.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0.");

            RuleFor(x => x.BookingSearchParams.PageSize)
                .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");

            RuleFor(x => x.BookingSearchParams)
                .Must(p => !p.FromDate.HasValue || !p.ToDate.HasValue || p.FromDate <= p.ToDate)
                .WithMessage("'FromDate' must be before or equal to 'ToDate'.");
        }
    }
}
