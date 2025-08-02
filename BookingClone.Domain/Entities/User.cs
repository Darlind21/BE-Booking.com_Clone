using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; private set; }
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;

        [EmailAddress]
        public string Email { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public string? Country { get; private set; }
        public DateTime CreatedOnUtc { get; init; } = DateTime.UtcNow;
        public DateTime? UpdatedOnUtc { get; private set; }


        private readonly List<Review> _reviews = [];
        public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

        //FIX: Add Bookings List

        public User()
        {
            // Parameterless constructor for EF Core
        }

        public User(string firstName, string lastName, string email, string passwordHash, string? country = null)
        {
            if (string.IsNullOrWhiteSpace(firstName)) throw new ArgumentException("First name is required");
            if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentException("Last name is required");
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@")) throw new ArgumentException("Invalid email");
            if (string.IsNullOrWhiteSpace(passwordHash) || passwordHash.Length < 8) throw new ArgumentException("Invalid password hash");

            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            Country = country;
        }
    }
}
