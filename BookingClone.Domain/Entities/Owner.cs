using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class Owner
    {
        [Key]
        public Guid Id { get; set; }
        public string IdCardNumber { get; set; } = null!;
        public string BankAccount { get; set; } = null!;
        public int PhoneNumber { get; set; }


        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public List<Apartment> Apartments { get; set; } = [];
    }
}
