using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Queries.GetBookingsForApartment
{
    public record GetBookingsForApartmentQuery : IRequest<Result<List<BookingResponseDTO>>>
    {
        public Guid ApartmentId { get; init; }
    }
}
