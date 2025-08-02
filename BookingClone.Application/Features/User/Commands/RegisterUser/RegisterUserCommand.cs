using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.RegisterUser
{
    //we user record because commands/queries represent an action or question; their props should be mutating after creation
    public record RegisterUserCommand : IRequest<Result<AuthenticatedUserDTO>>
        //We can think of IRequest as a message that the app sends to itself, It represents a Command/Query.
        //The type(TResponse) inside IRequest<TResponse> tells MediatR "When this request is handled, it will produce a result of type TResponse".

        //Result<T> provides a clean way to handle operation results without relying on exceptions for control flow
        //Rsult<T> represents the outcome of an operation that either -Succeeds(and returns T) or Fails (and carries one or more error messages)
    {
        public RegisterUserDTO RegisterUserDTO { get; init; } = default!;
        //
    }
}
