using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.ListNewApartment
{
    public record ListNewApartmentCommand : IRequest<Result<ApartmentResponseDTO>>
    {
        public ListNewApartmentDTO ListNewApartmentDTO { get; init; } = default!;
    }
}
