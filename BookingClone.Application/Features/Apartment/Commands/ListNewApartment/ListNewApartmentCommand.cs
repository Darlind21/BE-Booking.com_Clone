using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.ListNewApartment
{
    public record ListNewApartmentCommand : IRequest<Result<ListNewApartmentResponseDTO>>
    {
        public ListNewApartmentDTO ListNewApartmentDTO { get; init; } = default!;
    }
}
