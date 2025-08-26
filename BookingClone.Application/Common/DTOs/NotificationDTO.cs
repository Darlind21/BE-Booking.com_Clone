using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.DTOs
{
    public record NotificationDTO
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = null!;
        public string Message { get; init; } = null!;
        public bool IsRead { get; init; }
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ReadOnUtc { get; init; }
    }
}
