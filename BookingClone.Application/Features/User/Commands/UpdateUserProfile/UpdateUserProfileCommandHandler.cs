using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.UpdateUserProfile
{
    public class UpdateUserProfileCommandHandler (IUserRepository userRepository)
        : IRequestHandler<UpdateUserProfileCommand, Result<UserProfileDTO>>
    {
        public async Task<Result<UserProfileDTO>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var dto = request.UpdateUserProfileDTO;

            var userToUpdate = await userRepository.GetByIdAsync(dto.UserId);
            if (userToUpdate == null) throw new Exception ("Unable to find a user to update with this id");

            if (dto.FirstName != null) userToUpdate.UpdateFirstName(dto.FirstName);
            if (dto.LastName != null) userToUpdate.UpdateLastName(dto.LastName);
            if (dto.Country != null) userToUpdate.UpdateCountry(dto.Country);
            if (dto.Email != null) userToUpdate.UpdateEmail(dto.Email);
            userToUpdate.UpdateUpdatedOnUtc();

            var updated = await userRepository.UpdateAsync(userToUpdate);

            if (!updated) throw new Exception("Unable to update user profile");

            return new UserProfileDTO
            {
                UserId = userToUpdate.Id,
                FirstName = userToUpdate.FirstName,
                LastName = userToUpdate.LastName,
                Email = userToUpdate.Email,
                Country = userToUpdate.Country,
                CreatedOnUtc = userToUpdate.CreatedOnUtc,
                IsOwner = await userRepository.IsUserAnOwnerAsync(userToUpdate.Id)
            };
        }
    }
}
