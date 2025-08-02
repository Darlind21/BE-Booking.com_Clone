using BookingClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Repositories
{
    public interface IOwnerRepository : IBaseRepository<Owner>
    {
        Task<bool> BankAccountExistsAsync(string bankAccount);
        Task<bool> IdCardNumberExistsAsync(string idCardNumber);
        Task<bool> PhoneNumberExistsAsync(string phoneNumber);
        Task<Owner?> GetOwnerByUserIdAsync(Guid userId);
    }
}
