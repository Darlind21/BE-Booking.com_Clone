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
        public Guid Id { get; private set; }
        public DateOnly StartDate { get; private set; }
        public DateOnly EndDate { get; private set; }
        public decimal PriceForPeriod { get; private set; }
        public decimal CleaningFee { get; private set; }
        public decimal? AmenitiesUpCharge { get; private set; }
        public decimal TotalPrice { get; private set; }
        public BookingStatus Status { get; private set; } = BookingStatus.Pending;
        public DateTime CreatedOnUtc { get; init; } = DateTime.UtcNow;
        public DateTime? ConfirmedOnUtc { get; private set; }
        public DateTime? RejectedOnUtc { get; private set; }
        public DateTime? CancelledOnUtc { get; private set; }
        public DateTime? CompletedOnUtc { get; private set; }



        [ForeignKey(nameof(Apartment))]
        public Guid ApartmentId { get; private set; }
        public Apartment Apartment { get; private set; } = null!;


        [ForeignKey(nameof(User))]
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;


        [ForeignKey(nameof(Review))]
        public Guid? ReviewId { get; private set; }
        public Review? Review { get; private set; } = null!;


        public Booking()
        {
            // Parameterless constructor for EF Core
        }

        public Booking(DateOnly startDate, DateOnly endDate, decimal priceForPeriod,
                        decimal cleaningFee, Guid apartmentId,
                        Guid userId, decimal? amenitiesUpCharge = null)
        {
            if (startDate > endDate) throw new ArgumentException("StartDate cannot be later than EndDate");
            if (priceForPeriod < 0) throw new ArgumentException("Price cannot be negative");
            if (cleaningFee < 0) throw new ArgumentException("Cleaning fee cannot be negative");
            if (amenitiesUpCharge.HasValue && amenitiesUpCharge < 0) throw new ArgumentException("AmenitieUpcharge cannot be negative");

            Id = Guid.NewGuid();
            StartDate = startDate;
            EndDate = endDate;
            PriceForPeriod = priceForPeriod;
            CleaningFee = cleaningFee;
            TotalPrice = priceForPeriod + cleaningFee + amenitiesUpCharge.GetValueOrDefault();
            AmenitiesUpCharge = amenitiesUpCharge;


            ApartmentId = apartmentId;

            UserId = userId;
        }

        public void CancelBooking()
        {
            CancelledOnUtc = DateTime.UtcNow;
        }
        public void ConfirmBooking()
        {
            ConfirmedOnUtc = DateTime.UtcNow;
        }
        public void RejectBooking()
        {
            RejectedOnUtc = DateTime.UtcNow;
        }
        public void CompleteBooking()
        {
            CompletedOnUtc = DateTime.UtcNow;
        }

    }
}
