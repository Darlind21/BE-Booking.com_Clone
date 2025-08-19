using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.CompleteBooking
{
    public record CompleteBookingsCommand : IRequest<Result>
    {
        public Guid BookingId { get; init; }
    }

}
