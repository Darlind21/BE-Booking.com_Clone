using BCrypt.Net;
using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.LoginUser
{
    public class LoginUserCommandHandler(IUserRepository userRepository, ITokenService tokenService) : IRequestHandler<LoginUserCommand, Result<AuthenticatedUserDTO>>
    {
        public async Task<Result<AuthenticatedUserDTO>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.LoginUserDTO;

            var user = await userRepository.GetUserByEmailAsync(dto.Email);
            if (user == null) return Result.Fail("Invalid email");


            //.Verify() first extracts original salt from original passwordhash and it recomputes it using the salt 
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isValidPassword) return Result.Fail("Invalid password");

            return new AuthenticatedUserDTO
            {
                Email = user.Email,
                Token = tokenService.GenerateToken(user)
            };
        }
    }
}
