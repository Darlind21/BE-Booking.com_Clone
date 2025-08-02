using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Owner.Commands.RegisterOwner
{
    public class RegisterOwnerCommandValidator : AbstractValidator<RegisterOwnerCommand>
    {
        public RegisterOwnerCommandValidator()
        {
            When(x => x.RegisterOwnerDTO.IsExistingUser, () =>
            {
                RuleFor(x => x.RegisterOwnerDTO.ExistingUserId)
                .NotEmpty()
                .WithMessage("User id is required when registering existing user as owner");

                //If we have existing user, User properties must be null
                RuleFor(x => x.RegisterOwnerDTO.FirstName).Empty().WithMessage("First name must be empty when registering existing user as owner");

                RuleFor(x => x.RegisterOwnerDTO.LastName).Empty().WithMessage("Last name must be empty when registering existing user as owner");

                RuleFor(x => x.RegisterOwnerDTO.Email).Empty().WithMessage("Email name must be empty when registering existing user as owner");

                RuleFor(x => x.RegisterOwnerDTO.Password).Empty().WithMessage("Password must be empty when registering existing user as owner");

                RuleFor(x => x.RegisterOwnerDTO.Country).Empty().WithMessage("Country must be empty when registering existing user as owner");
            });

            When(x => !x.RegisterOwnerDTO.IsExistingUser, () =>
            {
                RuleFor(x => x.RegisterOwnerDTO.FirstName)
                    .NotEmpty().WithMessage("First name is required")
                    .MaximumLength(100).WithMessage("Maximum length for first name is 100 chars")
                    .MinimumLength(2).WithMessage("Minimum length for last name is 2 chars");

                RuleFor(x => x.RegisterOwnerDTO.LastName)
                    .NotEmpty().WithMessage("Last name is required")
                    .MaximumLength(100).WithMessage("Maximum length is 100 chars")
                    .MinimumLength(2).WithMessage("Minimum length for last name is 2 chars");

                RuleFor(x => x.RegisterOwnerDTO.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Email is invalid")
                    .MinimumLength(5).WithMessage("Minimum length for email is 5 chars")
                    .MaximumLength(250).WithMessage("Maximum length for email is 250 chars");


                RuleFor(x => x.RegisterOwnerDTO.Password)
                    .NotEmpty().WithMessage("Password is required")
                    .MinimumLength(8).WithMessage("Minimum length for password is 8 chars")
                    .MaximumLength(250).WithMessage("Maximum length for password is 250 chars");

                RuleFor(x => x.RegisterOwnerDTO.Country)
                    .MaximumLength(100).WithMessage("Maximum length for country is 100")
                    .When(x => x.RegisterOwnerDTO.Country != null);

                //if a new user is registering as owner directly the user id must not be provided
                RuleFor(x => x.RegisterOwnerDTO.ExistingUserId).Empty();
            });

            RuleFor(x => x.RegisterOwnerDTO.IdCardNumber)
                .NotEmpty().WithMessage("ID card number is required")
                .Length(9, 20).WithMessage("ID card number must be between 9 and 20 characters")
                .Matches(@"^[A-Za-z0-9]+$").WithMessage("ID card number can only contain letters and numbers");

            RuleFor(x => x.RegisterOwnerDTO.BankAccount)
                .NotEmpty().WithMessage("Account number is required")
                .Length(8, 17).WithMessage("Account number must be between 8 and 17 digits")
                .Matches(@"^\d+$").WithMessage("Account number must contain only digits");

            RuleFor(x => x.RegisterOwnerDTO.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9\s\-\(\)]{6,20}$").WithMessage("Invalid phone number format")
                .MinimumLength(6).WithMessage("Phone number too short")
                .MaximumLength(20).WithMessage("Phone number too long");
        }
    }
}
