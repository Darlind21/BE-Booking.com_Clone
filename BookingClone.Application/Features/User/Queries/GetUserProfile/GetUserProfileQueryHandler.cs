using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Queries.GetUserProfile
{
    public class GetUserProfileQueryHandler (IUserRepository userRepository)
        : IRequestHandler<GetUserProfileQuery, Result<UserProfileDTO>>
    {
        public async Task<Result<UserProfileDTO>> Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null) return Result.Fail("User with this id not found");

            var userProfileDTO = new UserProfileDTO
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Country = user.Country,
                CreatedOnUtc = user.CreatedOnUtc,
                IsOwner = await userRepository.IsUserAnOwnerAsync(user.Id)
            };

            return userProfileDTO;
        }
    }
}
