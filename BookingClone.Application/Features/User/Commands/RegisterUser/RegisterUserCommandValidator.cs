using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    //AbstractValidator<T> is a class provided by FluentValidation library to validate objects in a clean, readable and reusable way 
    //Main benefit it offers - it keeps validation logic seperate from business logic 
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.RegisterUserDTO.FirstName)
                .NotEmpty().WithMessage("First name is required") 
                //if the validation( .NotEmpty() fails FluentValidation creates a "ValidationFailure"
                //If we have a ValidationBehaviour(needs to be added manually via DI) and we have ValidationFailures it throws a ValidationException,
                //which contains all ValidationFailure objs
                .MaximumLength(100).WithMessage("Maximum length for first name is 100 chars")
                .MinimumLength(2).WithMessage("Minimum length for last name is 2 chars");

            RuleFor(x => x.RegisterUserDTO.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(100).WithMessage("Maximum length is 100 chars")
                .MinimumLength(2).WithMessage("Minimum length for last name is 2 chars");

            RuleFor(x => x.RegisterUserDTO.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is invalid")
                .MinimumLength(5).WithMessage("Minimum length for email is 5 chars")   
                .MaximumLength(250).WithMessage("Maximum length for email is 250 chars");


            RuleFor(x => x.RegisterUserDTO.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Minimum length for password is 8 chars")
                .MaximumLength(250).WithMessage("Maximum length for password is 250 chars");

            RuleFor(x => x.RegisterUserDTO.Country)
                .MaximumLength(100).WithMessage("Maximum length for country is 100")
                .When(x => x.RegisterUserDTO.Country != null);
        }
    }
}
