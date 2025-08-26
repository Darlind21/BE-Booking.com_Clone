using BookingClone.Application.Common.Enums;
using BookingClone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Helpers
{
    public record NotificationsSearchParams
    {
        public Guid UserId { get; init; }
        public int PageNumber { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public DateTime? FromDate { get; init; }
        public DateTime? ToDate { get; init; }
        public string SortBy { get; init; } = "Date";
        public bool SortDescending { get; init; } = true;
    }
}
