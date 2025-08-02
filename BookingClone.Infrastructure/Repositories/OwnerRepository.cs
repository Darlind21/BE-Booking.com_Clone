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
    public class OwnerRepository(BookingDbContext context) : BaseRepository<Owner>(context), IOwnerRepository
    {
        private readonly BookingDbContext context = context;

        public async Task<bool> BankAccountExistsAsync(string bankAccount)
        {
            return await context.Owners
                .AnyAsync(x => x.BankAccount == bankAccount);
        }

        public async Task<Owner?> GetOwnerByUserIdAsync(Guid userId)
        {
            return await context.Owners
                .FirstOrDefaultAsync(o => o.UserId == userId);
        }

        public async Task<bool> IdCardNumberExistsAsync(string idCardNumber)
        {
            return await context.Owners
                .AnyAsync(x => x.IdCardNumber == idCardNumber);
        }

        public async Task<bool> PhoneNumberExistsAsync(string phoneNumber)
        {
            return await context.Owners
                .AnyAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
