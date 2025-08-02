using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Owner.Commands.RegisterOwner
{
    public record RegisterOwnerDTO
    {
        //Optional props based on if its an existing user or a new user directly registering as owner
        public Guid? ExistingUserId { get; init; }
        public bool IsExistingUser => ExistingUserId.HasValue;

        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? Email { get; init; }
        public string? Password { get; init; } 
        public string? Country { get; init; }


        //Required props regardless if its an existing user or not
        public string IdCardNumber { get; init; } = null!;
        public string BankAccount { get; init; } = null!;
        public string PhoneNumber { get; init; } = null!;
    }
}
