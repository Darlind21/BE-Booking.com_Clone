using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Owner.Commands.RegisterOwner
{
    public record RegisterNewUserAsOwnerCommand : IRequest<Result<AuthenticatedUserDTO>>
    {
        public RegisterNewUserAsOwnerDTO RegisterNewUserAsOwnerDTO { get; init; } = default!;
    }
}
