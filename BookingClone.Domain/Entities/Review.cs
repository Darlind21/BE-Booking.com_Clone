using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class Review
    {
        [Key]
        public Guid Id { get; set; }
        public int RatingOutOfTen { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedOnUtc { get; init; } = DateTime.UtcNow;


        [ForeignKey(nameof(Booking))]
        public Guid BookingId { get; set; }
        public Booking Booking { get; set; } = null!;
    }
}
