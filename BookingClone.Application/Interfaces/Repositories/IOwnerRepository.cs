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
        Task<bool> BankAccountExists(string bankAccount);
        Task<bool> IdCardNumberExists(string idCardNumber);
        Task<bool> PhoneNumberExists(string phoneNumber);
    }
}
