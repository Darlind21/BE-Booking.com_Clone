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
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
        public string Address { get; private set; } = null!;
        public decimal Price { get; private set; } 
        public string Description { get; private set; } = null!;
        public decimal CleaningFee { get; private set; }
        public DateTime LastBookedOnUtc { get; private set; }


        private readonly List<string> _amenities = new (20); //An apartment can have at maximum 20 amenities
        public IReadOnlyCollection<string> Amenities => _amenities.AsReadOnly();


        private readonly List<ApartmentPhoto> _apartmentPhotos = [];
        public IReadOnlyCollection<ApartmentPhoto> ApartmentPhotos => _apartmentPhotos.AsReadOnly();


        private readonly List<Owner> _owners = [];
        public IReadOnlyCollection<Owner> Owners => _owners.AsReadOnly();


        private readonly List<Booking> _bookings= [];
        public IReadOnlyCollection<Booking> Bookings => _bookings.AsReadOnly();

        public Apartment()
        {
            
        }

        public Apartment(string name, string address, decimal price, string description, decimal cleaningFee )
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Apartment name cannot be null or empty");
            if (string.IsNullOrEmpty(address)) throw new ArgumentException("Apartment address cannot be null or empty");
            if (string.IsNullOrEmpty(description)) throw new ArgumentException("Apartment description cannot be null or empty");

            if (price < 0) throw new ArgumentException("Apartment price cannot be negative");
            if (cleaningFee < 0) throw new ArgumentException("Cleaning fee cannot be negative");


            Name = name;
            Address = address;
            Price = price;
            Description = description;
            CleaningFee = cleaningFee;
        }
    }
}
