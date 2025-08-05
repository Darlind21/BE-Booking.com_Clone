using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.Photos.AddApartmentPhoto
{
    public record AddPhotoDTO
    {
        public Guid ApartmentId { get; init; }
        public List<IFormFile> ApartmentPhotos { get; init; } = []; 
    }
}
