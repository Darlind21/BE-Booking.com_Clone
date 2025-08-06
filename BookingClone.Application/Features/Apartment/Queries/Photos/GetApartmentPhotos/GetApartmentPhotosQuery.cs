using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Queries.Photos.GetApartmentPhotos
{
    public record GetApartmentPhotosQuery : IRequest<Result<List<PhotoResponseDTO>>>
    {
        public Guid ApartmentId { get; init; }
    }
}
