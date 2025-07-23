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
        public Guid Id { get; private set; }
        public byte RatingOutOfTen { get; private set; }
        public string? Comment { get; private set; }
        public DateTime CreatedOnUtc { get; init; } = DateTime.UtcNow;


        [ForeignKey(nameof(Booking))]
        public Guid BookingId { get; private set; }
        public Booking Booking { get; private set; } = null!; // EF Core will initialize this property when loading data from the database

        public Review()
        {
            // Parameterless constructor for EF Core
        }

        public Review(byte ratingOutOfTen, Guid bookingId, string? comment = null)
        {
            if (ratingOutOfTen < 1 || ratingOutOfTen > 10) throw new ArgumentException("Rating must be between 1 and 10");
 
            Id = Guid.NewGuid();
            RatingOutOfTen = ratingOutOfTen;

            BookingId = bookingId;
        }
    }
}
