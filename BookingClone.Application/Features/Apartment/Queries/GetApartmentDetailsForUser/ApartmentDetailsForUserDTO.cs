using BookingClone.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Queries.GetApartmentDetails
{
    public record ApartmentDetailsForUserDTO
    {
        public string Name { get; init; } = null!;
        public string Address { get; init; } = null!;
        public decimal PricePerDay { get; init; }
        public string Description { get; init; } = null!;
        public decimal CleaningFee { get; init; }

        public List<PhotoResponseDTO> ApartmentPhotos { get; init; } = [];
        public List<string> Amenities { get; init; } = [];

        //IMPROVE: Add list of reviews from bookings list for the apartment
    }
}
