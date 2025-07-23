using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public RegisterUserDTO RegisterUserDTO { get; set; } = default!;
        //
    }
}
