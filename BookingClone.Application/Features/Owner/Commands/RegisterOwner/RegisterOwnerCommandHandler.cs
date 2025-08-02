using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Features.Owner.Commands.RegisterUserAsOwner;
using BookingClone.Application.Features.User.Commands.RegisterUser;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Owner.Commands.RegisterOwner
{
    public class RegisterOwnerCommandHandler
        (ISender sender)
        : IRequestHandler<RegisterOwnerCommand, Result<AuthenticatedUserDTO>>
    {
        public async Task<Result<AuthenticatedUserDTO>> Handle(RegisterOwnerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterOwnerDTO;

            if (dto.IsExistingUser)
            {
                var registerUserAsOwnerDTO = new RegisterUserAsOwnerDTO
                {
                    ExistingUserId = dto.ExistingUserId!.Value,
                    IdCardNumber = dto.IdCardNumber,
                    BankAccount = dto.BankAccount,
                    PhoneNumber = dto.PhoneNumber
                };

                var registerUserAsOwnerCommand = new RegisterUserAsOwnerCommand { RegisterUserAsOwnerDTO = registerUserAsOwnerDTO };
                var registerUserAsOwnerResult = await sender.Send(registerUserAsOwnerCommand);


                return registerUserAsOwnerResult;
            }
            else
            {
                var registerUserDTO = new RegisterUserDTO
                {
                    FirstName = dto.FirstName!,
                    LastName = dto.LastName!,
                    Email = dto.Email!,
                    Password = dto.Password!,
                    Country = dto.Country
                };

                var registerUserCommand = new RegisterUserCommand { RegisterUserDTO = registerUserDTO };
                var registerUserResult = await sender.Send(registerUserCommand);

                if (registerUserResult.IsFailed)
                    return Result.Fail(string.Join("; ", registerUserResult.Errors.Select(x => x.Message)));



                var registerUserAsOwnerDTO = new RegisterUserAsOwnerDTO
                {
                    ExistingUserId = registerUserResult.Value.UserId,
                    IdCardNumber = dto.IdCardNumber,
                    BankAccount = dto.BankAccount,
                    PhoneNumber = dto.PhoneNumber
                };

                var registerUserAsOwnerCommand = new RegisterUserAsOwnerCommand { RegisterUserAsOwnerDTO = registerUserAsOwnerDTO };
                var registerUserAsOwnerResult = await sender.Send(registerUserAsOwnerCommand);


                return registerUserAsOwnerResult;
            }
        }
    }
}
