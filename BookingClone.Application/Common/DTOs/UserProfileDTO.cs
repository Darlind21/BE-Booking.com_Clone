using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.DTOs
{
    public record UserProfileDTO
    {
        [Key]
        public Guid UserId { get; init; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string? Country { get; init; }
        public DateTime CreatedOnUtc { get; init; }
        public bool IsOwner { get; init; } //in case front end needs to display button e.g "Go to owner dashboard"
    }
}
