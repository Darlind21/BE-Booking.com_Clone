using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.DTOs
{
    public record AuthenticatedUserDTO
    {
        public Guid UserId { get; set; }
        //including id in case frontend wants to use it in the futures
        public string FirstName { get; set; } = null!; 
        //including first name in case front end might want to display in navbar 
        public string Email { get; init; } = null!;
        public string Token { get; init; } = null!;
    }
}
