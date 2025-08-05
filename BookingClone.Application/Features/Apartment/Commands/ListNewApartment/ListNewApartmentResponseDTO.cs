using BookingClone.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.ListNewApartment
{
    public record ListNewApartmentResponseDTO
    {
        public Guid ApartmentId { get; init; }
        public string Name { get; init; } = null!;
        public string Address { get; init; } = null!;
        public decimal PricePerDay { get; init; }
        public string Description { get; init; } = null!;
        public decimal CleaningFee { get;init  ; }

        public List<string> Amenities { get; init; } = [];
    }
}
