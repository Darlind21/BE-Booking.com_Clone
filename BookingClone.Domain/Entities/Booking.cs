using BookingClone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal PriceForPeriod { get; set; }
        public decimal CleaningFee { get; set; }
        public decimal? AmenitiesUpCharge { get; set; }
        public decimal TotalPrice { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime CreatedOnUtc { get; init; } = DateTime.UtcNow;
        public DateTime? ConfirmedOnUtc { get; set; }
        public DateTime? RejectedOnUtc { get; set; }
        public DateTime? CompletedOnUtc { get; set; }



        [ForeignKey(nameof(Apartment))]
        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = null!;


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;


        [ForeignKey(nameof(Review))]
        public Guid? ReviewId { get; set; }
        public Review? Review { get; set; }
    }
}
