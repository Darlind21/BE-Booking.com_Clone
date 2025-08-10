using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.CreateBooking
{
    public record CreateBookingDTO
    {
        public Guid UserId { get; init; }
        public Guid ApartmentId { get; init; }
        public DateOnly CheckinDate { get; init; }
        public DateOnly CheckoutDate { get; init; }
    }
}
