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
        public async Task<bool> BankAccountExists(string bankAccount)
        {
            return await context.Owners
                .AnyAsync(x => x.BankAccount == bankAccount);
        }

        public async Task<bool> IdCardNumberExists(string idCardNumber)
        {
            return await context.Owners
                .AnyAsync(x => x.IdCardNumber == idCardNumber);
        }

        public async Task<bool> PhoneNumberExists(string phoneNumber)
        {
            return await context.Owners
                .AnyAsync(x => x.PhoneNumber == phoneNumber);
        }
    }
}
