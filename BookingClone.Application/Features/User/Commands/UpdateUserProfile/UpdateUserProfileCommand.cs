using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.UpdateUserProfile
{
    public record UpdateUserProfileCommand : IRequest<Result<UserProfileDTO>>
    {
        public UpdateUserProfileDTO UpdateUserProfileDTO { get; init; } = default!;
    }
}
