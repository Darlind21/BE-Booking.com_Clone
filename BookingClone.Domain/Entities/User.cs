using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Country { get; set; }
        public DateTime CreatedOnUtc { get; init; } = DateTime.UtcNow;
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
