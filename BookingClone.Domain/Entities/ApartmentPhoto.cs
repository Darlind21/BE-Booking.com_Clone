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
        public Guid Id { get; set; }
        public string ImageBase64 { get; set; } = null!;
        public string ImageName { get; set; } = null!;
        public string ImageType { get; set; } = null!;
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;


        [ForeignKey(nameof(Apartment))]
        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; } = null!;
    }
}
