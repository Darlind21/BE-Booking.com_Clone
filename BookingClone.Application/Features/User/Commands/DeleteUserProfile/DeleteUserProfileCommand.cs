using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.DeleteUserProfile
{
    public record DeleteUserProfileCommand : IRequest<Result<bool>>
    {
        public Guid UserId { get; init; }
    }
}
