using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Features.User.Commands.LoginUser.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.LoginUser
{
    public record LoginUserCommand : IRequest<Result<AuthenticatedUserDTO>>
    {
        public LoginUserDTO LoginUserDTO { get; set; } = default!;
    }
}
