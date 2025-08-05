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

        //Url to access the image from the cloudinary service
        public string Url { get; private set; } = null!;
        public bool IsMainPhoto { get; private set; } = false;

        //Cloudinary's public id for the image, used to delete/manage the image from the cloudinary service
        public string PublicId { get; private set; } = null!;
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;


        [ForeignKey(nameof(Apartment))]
        public Guid ApartmentId { get; private set; }
        public Apartment Apartment { get; private set; } = null!; // EF Core will initialize this property when loading data from the database

        protected ApartmentPhoto() { }//parameterless ctor for ef core to use when initializing the entity when it loads data from the database

        public ApartmentPhoto(string url, string publicId, Guid apartmentId)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentException("url cannot be null or empty");
            if (string.IsNullOrEmpty(publicId)) throw new ArgumentException("publicId cannot be null or empty");


            Id = Guid.NewGuid();
            Url = url;
            PublicId = publicId;
            ApartmentId = apartmentId;
        }
    }
}
