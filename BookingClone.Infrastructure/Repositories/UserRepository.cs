using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Domain.Entities;
using BookingClone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Repositories
{
    public class UserRepository(BookingDbContext context) : BaseRepository<User>(context), IUserRepository
    {
        private readonly BookingDbContext context = context;

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await context.Users
                .SingleOrDefaultAsync(u => u.Email == email);
        }
    }
}
