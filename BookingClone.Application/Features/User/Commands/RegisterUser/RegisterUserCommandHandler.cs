using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.RegisterUser
{
    public class RegisterUserCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, Guid>
    {

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var dto = request.RegisterUserDTO;

            var existingUser = await userRepository.GetUserByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new InvalidOperationException("User with this email already exists");

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new Domain.Entities.User(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                hashedPassword,
                dto.Country
            );

            var created = await userRepository.AddAsync(user);

            return user.Id;
        }
    }
}
