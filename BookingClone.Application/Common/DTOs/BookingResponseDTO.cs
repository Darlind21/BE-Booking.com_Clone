using BookingClone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.DTOs
{
    public record BookingResponseDTO
    {
        public Guid BookingId { get; init; }
        public Guid ApartmentId { get; init; }
        public string ApartmentName { get; init; } = default!;
        public string ApartmentAddress { get; init; } = default!;
        public DateOnly CheckinDate { get; init; }
        public DateOnly CheckoutDate { get; init; }
        public decimal PriceForPeriod { get; init; }
        public decimal CleaningFee { get; init; }
        public decimal? AmenitiesUpCharge { get; init; }
        public decimal TotalPrice { get; init; }
        public string Status { get; init; } = default!;
    }
}
