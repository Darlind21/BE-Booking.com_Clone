using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.User.Commands.LoginUser.DTOs
{
    public record LoginUserDTO
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
