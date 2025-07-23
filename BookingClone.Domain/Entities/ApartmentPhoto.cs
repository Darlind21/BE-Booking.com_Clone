using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class ApartmentPhoto
    {
        [Key]
        public Guid Id { get; private set; }
        public string ImageBase64 { get; private set; } = null!;
        public string ImageName { get; private set; } = null!;
        public string ImageType { get; private set; } = null!;
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;


        [ForeignKey(nameof(Apartment))]
        public Guid ApartmentId { get; private set; }
        public Apartment Apartment { get; private set; } = null!; // EF Core will initialize this property when loading data from the database

        protected ApartmentPhoto() { }//parameterless ctor for ef core to use when initializing the entity when it loads data from the database

        public ApartmentPhoto(string imageBase64, string imageName, string imageType, Guid apartmentId)
        {
            if (string.IsNullOrEmpty(imageBase64)) throw new ArgumentException("imageBase64 cannot be null or empty");
            if (string.IsNullOrEmpty(imageName)) throw new ArgumentException("imageName cannot be null or empty");
            if (string.IsNullOrEmpty(imageType)) throw new ArgumentException("imageType cannot be null or emptys");


            Id = Guid.NewGuid();
            ImageBase64 = imageBase64;
            ImageName = imageName;
            ImageType = imageName;
            ApartmentId = apartmentId;
        }
    }
}
