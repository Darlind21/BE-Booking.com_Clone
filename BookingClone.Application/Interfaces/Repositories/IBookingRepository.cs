using BookingClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Repositories
{
    public interface IBookingRepository : IBaseRepository<Booking>
    {
        Task<List<Booking>> GetBookingsByUserId(Guid userId);
        Task<List<Booking>> GetBookingsByApartmentId(Guid apartmentId);
    }
}
