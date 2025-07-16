using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class Apartment
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public decimal CleaningFee { get; set; }
        public DateTime LastBookedOnUtc { get; set; }
        public List<string> Amenities { get; set; } = new (20); //An apartment can have at maximum 20 amenities

        public List<ApartmentPhoto> ApartmentPhotos { get; set; } = [];
        public List<Owner> Owners { get; set; } = [];
        public List<Booking> Bookings { get; set; } = [];
    }
}
