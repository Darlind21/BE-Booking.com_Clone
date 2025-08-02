using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.DeleteUserProfile
{
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, Result<bool>>
    {
        public Task<Result<bool>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
