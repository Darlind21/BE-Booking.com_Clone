using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator()
        {
            RuleFor(x => x.CreateBookingDTO).NotNull().WithMessage("Unable to create booking. No booking data provided");

            When(x => x.CreateBookingDTO != null, () =>
            {
                RuleFor(x => x.CreateBookingDTO.UserId)
                    .NotEmpty().WithMessage("UserId is required.");

                RuleFor(x => x.CreateBookingDTO.ApartmentId)
                    .NotEmpty().WithMessage("ApartmentId is required.");

                RuleFor(x => x.CreateBookingDTO.CheckinDate)
                    .Must(date => date > DateOnly.FromDateTime(DateTime.Today))
                    .WithMessage("Checkin date cannot be today or in the past.");

                RuleFor(x => x.CreateBookingDTO.CheckoutDate)
                    .GreaterThan(x => x.CreateBookingDTO.CheckinDate)
                    .WithMessage("Checkout date must be after checkin date.");
            });
        }
    }
}
