using BookingClone.Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Helpers
{
    public record SearchParams //used to encapsulatte filters, sorting and pagination parameters
    {
        public string? Name { get; init; }
        public string? Address { get; init; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateOnly CheckInDate { get; init; }
        public DateOnly CheckoutDate { get; init; }

        public ApartmentSortBy SortBy { get; init; } = ApartmentSortBy.Popularity; //popularity is apartments with the most bookings
        public bool SortDescending { get; init; } = false;

        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
    }
}
