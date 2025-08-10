using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.CreateBooking
{
    public record CreateBookingCommand : IRequest<Result<BookingResponseDTO>>
    {
        public CreateBookingDTO CreateBookingDTO { get; init; } = default!;
    }
}
