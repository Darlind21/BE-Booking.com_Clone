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
        //when getting a single apt we load all its photos as well 
        public List<string> Amenities { get; init; } = [];

        public decimal? AverageRating { get; init; }
    }
}
