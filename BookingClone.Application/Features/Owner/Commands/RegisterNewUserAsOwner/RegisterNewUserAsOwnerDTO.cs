using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Owner.Commands.RegisterOwner
{
    public record RegisterNewUserAsOwnerDTO
    {
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string Password { get; init; } = null!;
        public string? Country { get; init; }
        public string IdCardNumber { get; init; } = null!;
        public string BankAccount { get; init; } = null!;
        public string PhoneNumber { get; init; } = null!;
    }
}
