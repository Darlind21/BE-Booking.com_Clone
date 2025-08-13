using BookingClone.Application.Common.Enums;
using BookingClone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Helpers
{
    public record ReviewSearchParams
    {
        public Guid ApartmentId { get; init; }

        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;


        public DateOnly? FromDate { get; init; }
        public DateOnly? ToDate { get; init; }
        public byte? MinRating { get; init; }
        public byte? MaxRating { get; init; }
        public ReviewSortBy SortBy { get; init; } = ReviewSortBy.CreatedOn;
        public bool SortDescending { get; init; } = true;
    }
}
