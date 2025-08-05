using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Features.Owner.Commands.RegisterUserAsOwner;
using BookingClone.Application.Features.User.Commands.DeleteUserProfile;
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
    public class RegisterNewUserAsOwnerCommandHandler
        (IOwnerRepository ownerRepository, IUserRepository userRepository, ITokenService tokenService)
        : IRequestHandler<RegisterNewUserAsOwnerCommand, Result<AuthenticatedUserDTO>>
    {
        public async Task<Result<AuthenticatedUserDTO>> Handle(RegisterNewUserAsOwnerCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterNewUserAsOwnerDTO;

            var existingUser = await userRepository.ExistsAsync(x => x.Email == dto.Email);
            if (existingUser) return Result.Fail("User with this email already exists");

            if (await ownerRepository.BankAccountExistsAsync(dto.BankAccount)) return Result.Fail("An owner with this bank account already exists");

            if (await ownerRepository.IdCardNumberExistsAsync(dto.IdCardNumber)) return Result.Fail("An owner with this id card number already exists");

            if (await ownerRepository.PhoneNumberExistsAsync(dto.PhoneNumber)) return Result.Fail("An owner with this phone number already exists");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var newUser = new Domain.Entities.User(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                hashedPassword,
                dto.Country
            );

            var newOwner = new Domain.Entities.Owner(dto.IdCardNumber, dto.BankAccount, dto.PhoneNumber, newUser.Id);
            newOwner.SetUser(newUser);

            var created = await ownerRepository.AddAsync(newOwner);
            if (!created) throw new Exception("Unable to create owner");

            return new AuthenticatedUserDTO
            {
                UserId = newUser.Id,
                FirstName = newUser.FirstName,
                Email = newUser.Email,
                Token = tokenService.GenerateToken(newUser, Domain.Enums.AppRole.Owner)
            };
        }
    }
}
