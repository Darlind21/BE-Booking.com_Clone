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
        public Guid Id { get; private set; }
        public string IdCardNumber { get; private set; } = null!;
        public string BankAccount { get; private set; } = null!;
        public string PhoneNumber { get; private set; } = null!;


        [ForeignKey(nameof(User))]
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        private readonly List<Apartment> _apartments = [];
        public IReadOnlyCollection<Apartment> Apartments => _apartments.AsReadOnly();

        public Owner()
        {
            // Parameterless constructor for EF Core
        }

        public Owner(string idCardNumber, string bankAccount, string phoneNumber, Guid userId)
        {
            if (string.IsNullOrEmpty(idCardNumber)) throw new ArgumentException("IdCardNumber is required");
            if (string.IsNullOrEmpty(bankAccount)) throw new ArgumentException("BankAccount is required");
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentException("PhoneNumber is required");


            Id = Guid.NewGuid();
            IdCardNumber = idCardNumber;
            BankAccount = bankAccount;
            PhoneNumber = phoneNumber;

            UserId = userId;
        }

        public void SetUser(User user)
        {
            User = user;
        }
    }
}
