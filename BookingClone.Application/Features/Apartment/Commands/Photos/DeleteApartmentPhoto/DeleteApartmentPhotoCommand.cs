using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.Photos.DeleteApartmentPhoto
{
    public record DeleteApartmentPhotoCommand : IRequest<Result>
    {
        public DeleteApartmentPhotoDTO DeleteApartmentPhotoDTO { get; init; } = default!;
    }
}
