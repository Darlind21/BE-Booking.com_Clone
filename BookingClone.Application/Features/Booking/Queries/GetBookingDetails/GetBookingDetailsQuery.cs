using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Queries.GetBookingDetails
{
    public record GetBookingDetailsQuery : IRequest<Result<BookingResponseDTO>>
    {
        public Guid BookingId { get; init; }
    }
}
