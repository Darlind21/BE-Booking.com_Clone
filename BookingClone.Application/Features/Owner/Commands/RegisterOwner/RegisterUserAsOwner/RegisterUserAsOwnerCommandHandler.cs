using BookingClone.Application.Common.DTOs;
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
    public class RegisterUserAsOwnerCommandHandler
        (IUserRepository userRepository, ITokenService tokenService, IOwnerRepository ownerRepository) 
        : IRequestHandler<RegisterUserAsOwnerCommand, Result<AuthenticatedUserDTO>>
    {
        public async Task<Result<AuthenticatedUserDTO>>Handle(RegisterUserAsOwnerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterUserAsOwnerDTO;


                var existingUser = await userRepository.GetByIdAsync(dto.ExistingUserId); 
                //loading user instead of checking if it just exists to get email to generate token
                if (existingUser == null) return Result.Fail("User with this id does not exist");

                if (await userRepository.IsUserAnOwnerAsync(dto.ExistingUserId)) return Result.Fail("User is already owner");

                if (await ownerRepository.BankAccountExists(dto.BankAccount)) return Result.Fail("An owner with this bank account already exists");

                if (await ownerRepository.IdCardNumberExists(dto.IdCardNumber)) return Result.Fail("An owner with this id card number already exists");

                if (await ownerRepository.PhoneNumberExists(dto.PhoneNumber)) return Result.Fail("An owner with this phone number already exists");

                var newOwner = new Domain.Entities.Owner(dto.IdCardNumber, dto.BankAccount, dto.PhoneNumber, dto.ExistingUserId);

                var created = await ownerRepository.AddAsync(newOwner);
                if (!created) throw new Exception("Unable to create owner");

                return new AuthenticatedUserDTO
                {
                    UserId = existingUser.Id,
                    FirstName = existingUser.FirstName,
                    Email = existingUser.Email,
                    Token = tokenService.GenerateToken(existingUser, Domain.Enums.AppRole.Owner)
                };
        }
    }
}
