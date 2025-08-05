using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Owner.Commands.RegisterUserAsOwner
{
    public record RegisterUserAsOwnerDTO
    {
        public Guid ExistingUserId { get; init; }

        public string IdCardNumber { get; init; } = null!;
        public string BankAccount { get; init; } = null!;
        public string PhoneNumber { get; init; } = null!;
    }
}
