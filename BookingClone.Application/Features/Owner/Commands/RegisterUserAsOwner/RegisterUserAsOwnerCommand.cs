using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Features.Owner.Commands.RegisterUserAsOwner;
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
    public record RegisterUserAsOwnerCommand : IRequest<Result<AuthenticatedUserDTO>>
    {
        public RegisterUserAsOwnerDTO RegisterUserAsOwnerDTO { get; init; } = default!;
    }
}
