using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand> //implements IValidator
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.LoginUserDTO.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is invalid")
                .MinimumLength(5).WithMessage("Minimum length for email is 5 chars")
                .MaximumLength(250).WithMessage("Maximum length for email is 250 chars");

            RuleFor(x => x.LoginUserDTO.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Minimum length for password is 8 chars")
                .MaximumLength(250).WithMessage("Maximum length for password is 250 chars");
        }
    }
}
