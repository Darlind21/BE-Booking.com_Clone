using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Queries.GetUserProfile
{
    public record GetUserProfileQuery : IRequest<Result<UserProfileDTO>>
    {
        public Guid UserId { get; init; }
    }
}
