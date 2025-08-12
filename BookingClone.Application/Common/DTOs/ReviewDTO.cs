using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.DTOs
{
    public record ReviewDTO
    {
        public byte RatingOutOfTen { get; init; }
        public string? Comment { get; init; }
        public Guid BookingId { get; init; }
        public Guid? ReviewId { get; init; }
    }
}
