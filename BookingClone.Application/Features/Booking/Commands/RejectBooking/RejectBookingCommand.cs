using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.RejectBooking
{
    public record RejectBookingCommand : IRequest<Result>
    {
        public Guid UserId { get; init; }
        public Guid BookingId { get; init; }
    }
}
