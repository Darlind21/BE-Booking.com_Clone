using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        public UpdateUserProfileCommandValidator()
        {
            RuleFor(x => x.UpdateUserProfileDTO).NotNull().WithMessage("No data sent to update user profile");

            When(x => x.UpdateUserProfileDTO != null, () =>
            {
                RuleFor(x => x.UpdateUserProfileDTO.UserId)
                    .NotEmpty().WithMessage("UserId is required.");

                RuleFor(x => x.UpdateUserProfileDTO.FirstName)
                    .NotEmpty().WithMessage("First name is required")
                    .MaximumLength(100).WithMessage("Maximum length for first name is 100 chars")
                    .MinimumLength(2).WithMessage("Minimum length for last name is 2 chars")
                    .When(c => c.UpdateUserProfileDTO.FirstName != null);

                RuleFor(x => x.UpdateUserProfileDTO.LastName)
                    .NotEmpty().WithMessage("Last name is required")
                    .MaximumLength(100).WithMessage("Maximum length is 100 chars")
                    .MinimumLength(2).WithMessage("Minimum length for last name is 2 chars")
                    .When(c => c.UpdateUserProfileDTO.LastName != null);

                RuleFor(x => x.UpdateUserProfileDTO.Email)
                    .NotEmpty().WithMessage("Email is required")
                    .EmailAddress().WithMessage("Email is invalid")
                    .MinimumLength(5).WithMessage("Minimum length for email is 5 chars")
                    .MaximumLength(250).WithMessage("Maximum length for email is 250 chars")
                    .When(c => c.UpdateUserProfileDTO.Email != null);

                RuleFor(x => x.UpdateUserProfileDTO.Country)
                    .MaximumLength(100).WithMessage("Maximum length for country is 100")
                    .When(c => c.UpdateUserProfileDTO.Country != null);
            });
        }
    }
}
