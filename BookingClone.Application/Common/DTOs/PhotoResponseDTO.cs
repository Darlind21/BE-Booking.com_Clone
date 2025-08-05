using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.DTOs
{
    public record PhotoResponseDTO
    {
        public Guid ApartmentPhotoId { get; init; }
        public string Url { get; init; } = null!;
        public string PublicId { get; init; } = null!;
    }
}
