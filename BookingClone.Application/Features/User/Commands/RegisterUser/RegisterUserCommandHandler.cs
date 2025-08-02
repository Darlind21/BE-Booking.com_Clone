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

namespace BookingClone.Application.Features.User.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(IUserRepository userRepository, ITokenService tokenService) : IRequestHandler<RegisterUserCommand, Result<AuthenticatedUserDTO>>
    {

        public async Task<Result<AuthenticatedUserDTO>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            //A CancellationToken is like having an "emergency stop" btn for request processing... it is a way to ask the handler to stop working if the request is no longer needed
            // i.e. - user closed their browser tab - request timeout - app shutdown etc
        {
            var dto = request.RegisterUserDTO;

            var existingUser = await userRepository.ExistsAsync(x => x.Email == dto.Email); //returning Fluent Results for business logic validations
            if (existingUser) return Result.Fail("User with this email already exists");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            //Uses the BCrypt alogrithm, which is a password-specific hashing function designed to be slow and resistant to brute force
            //It automatically generates a random salt for each password and appends the salt into tghe final hash, so we do not need to store it seperately
            //We dont deal manually with salt or keys - just storing the final hash string.

            var user = new Domain.Entities.User(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                hashedPassword,
                dto.Country
            );

            var created = await userRepository.AddAsync(user);
            if (!created) throw new Exception("Unable to create new user");

            return new AuthenticatedUserDTO //if we'd be using older fluentResults versions we'd have to wrap it in Results.Ok(new ... {})
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                Email = user.Email,
                Token = tokenService.GenerateToken(user, Domain.Enums.AppRole.User)
            };
        }
    }
}
