using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.DTOs
{
    public record ApartmentResponseDTO
    {
        public Guid ApartmentId { get; init; }
        public string Name { get; init; } = null!;
        public string Address { get; init; } = null!;
        public decimal PricePerDay { get; init; }
        public string Description { get; init; } = null!;
        public decimal CleaningFee { get; init; }

        public List<string> Amenities { get; init; } = [];

        public PhotoResponseDTO MainPhoto { get; init; } = default!;
        //when loading a pagedlist of apartments or after just listing a new apt we only get the main photo
    }
}
