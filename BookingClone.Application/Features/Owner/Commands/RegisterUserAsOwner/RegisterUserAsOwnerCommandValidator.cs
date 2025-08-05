using BookingClone.Application.Features.Owner.Commands.RegisterOwner;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Owner.Commands.RegisterUserAsOwner
{
    public class RegisterUserAsOwnerCommandValidator : AbstractValidator<RegisterUserAsOwnerCommand>
    {
        public RegisterUserAsOwnerCommandValidator()
        {
            RuleFor(x => x.RegisterUserAsOwnerDTO.ExistingUserId)
                    .NotEmpty().WithMessage("UserId is required when registering existing user as owner");

            RuleFor(x => x.RegisterUserAsOwnerDTO.IdCardNumber)
                    .NotEmpty().WithMessage("ID card number is required")
                    .Length(9, 20).WithMessage("ID card number must be between 9 and 20 characters")
                    .Matches(@"^[A-Za-z0-9]+$").WithMessage("ID card number can only contain letters and numbers");

            RuleFor(x => x.RegisterUserAsOwnerDTO.BankAccount)
                    .NotEmpty().WithMessage("Account number is required")
                    .Length(8, 17).WithMessage("Account number must be between 8 and 17 digits")
                    .Matches(@"^\d+$").WithMessage("Account number must contain only digits");

            RuleFor(x => x.RegisterUserAsOwnerDTO.PhoneNumber)
                    .NotEmpty().WithMessage("Phone number is required")
                    .Matches(@"^\+?[0-9\s\-\(\)]{6,20}$").WithMessage("Invalid phone number format")
                    .MinimumLength(6).WithMessage("Phone number too short")
                    .MaximumLength(20).WithMessage("Phone number too long");            
        }

    }
}
