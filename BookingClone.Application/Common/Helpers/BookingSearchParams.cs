using BookingClone.Application.Common.Enums;
using BookingClone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Helpers
{
    public record BookingSearchParams
    {
        public Guid? UserId { get; init; }
        public Guid? ApartmentId { get; init; }

        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;


        public DateOnly? FromDate { get; init; }
        public DateOnly? ToDate { get; init; }
        public BookingStatus? Status { get; init; } 
        public BookingSortBy SortBy { get; init; } = BookingSortBy.CreatedAt;
        public bool SortDescending { get; init; } = true;
    }
}
