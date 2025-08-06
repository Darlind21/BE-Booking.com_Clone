using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.Photos.DeleteApartmentPhoto
{
    public record DeleteApartmentPhotoDTO
    {
        public Guid PhotoId { get; init; }
        public Guid ApartmentId { get; init; }
        public string PublicId { get; init; } = null!;
    }
}
