using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Queries.SearchApartments
{
    public record SearchApartmentsQuery : IRequest<Result<PagedList<ApartmentResponseDTO>>>
    {
        public SearchParams SearchParams { get; init; } = default!;
    }
}
