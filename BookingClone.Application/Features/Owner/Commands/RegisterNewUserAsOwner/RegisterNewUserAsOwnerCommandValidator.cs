using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Owner.Commands.RegisterOwner
{
    public class RegisterNewUserAsOwnerCommandValidator : AbstractValidator<RegisterNewUserAsOwnerCommand>
    {
        public RegisterNewUserAsOwnerCommandValidator()
        {
            RuleFor(x => x.RegisterNewUserAsOwnerDTO.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(100).WithMessage("Maximum length for first name is 100 chars")
                .MinimumLength(2).WithMessage("Minimum length for last name is 2 chars");

            RuleFor(x => x.RegisterNewUserAsOwnerDTO.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Maximum length is 100 chars")
                .MinimumLength(2).WithMessage("Minimum length for last name is 2 chars");

            RuleFor(x => x.RegisterNewUserAsOwnerDTO.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is invalid")
                .MinimumLength(5).WithMessage("Minimum length for email is 5 chars")
                .MaximumLength(250).WithMessage("Maximum length for email is 250 chars");


            RuleFor(x => x.RegisterNewUserAsOwnerDTO.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Minimum length for password is 8 chars")
                .MaximumLength(250).WithMessage("Maximum length for password is 250 chars");

            RuleFor(x => x.RegisterNewUserAsOwnerDTO.Country)
                .MaximumLength(100).WithMessage("Maximum length for country is 100")
                .When(x => x.RegisterNewUserAsOwnerDTO.Country != null);

            RuleFor(x => x.RegisterNewUserAsOwnerDTO.IdCardNumber)
                .NotEmpty().WithMessage("ID card number is required")
                .Length(9, 20).WithMessage("ID card number must be between 9 and 20 characters")
                .Matches(@"^[A-Za-z0-9]+$").WithMessage("ID card number can only contain letters and numbers");

            RuleFor(x => x.RegisterNewUserAsOwnerDTO.BankAccount)
                .NotEmpty().WithMessage("Account number is required")
                .Length(8, 17).WithMessage("Account number must be between 8 and 17 digits")
                .Matches(@"^\d+$").WithMessage("Account number must contain only digits");

            RuleFor(x => x.RegisterNewUserAsOwnerDTO.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9\s\-\(\)]{6,20}$").WithMessage("Invalid phone number format")
                .MinimumLength(6).WithMessage("Phone number too short")
                .MaximumLength(20).WithMessage("Phone number too long");
        }
    }
}
