using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Queries.GetApartmentDetails
{
    public record GetApartmentDetailsForUserQuery : IRequest<Result<ApartmentDetailsForUserDTO>>
    {
        public Guid ApartmentId { get; init; }
    }
}
