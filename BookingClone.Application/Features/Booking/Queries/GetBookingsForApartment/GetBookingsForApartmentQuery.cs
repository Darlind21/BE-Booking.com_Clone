using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Queries.GetBookingsForApartment
{
    public record GetBookingsForApartmentQuery : IRequest<Result<PagedList<BookingResponseDTO>>>
    {
        public Guid UserId { get; init; }
        public BookingSearchParams BookingSearchParams { get; init; } = default!;
    }
}
