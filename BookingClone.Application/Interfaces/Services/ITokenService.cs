using BookingClone.Domain.Entities;
using BookingClone.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user, AppRole userRole);
    }
}
