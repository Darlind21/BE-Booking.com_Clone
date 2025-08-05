using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.Photos.AddApartmentPhoto
{
    public record AddApartmentPhotoCommand : IRequest<Result<List<PhotoResponseDTO>>>
    {
        public AddPhotoDTO AddPhotoDTO { get; init; } = default!;
    }
}
