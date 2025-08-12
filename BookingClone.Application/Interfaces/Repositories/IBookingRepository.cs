using BookingClone.Application.Common.Helpers;
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
        IQueryable<Booking> GetBookingsByUserId(BookingSearchParams bookingSearchParams);
        IQueryable<Booking> GetBookingsByApartmentId(BookingSearchParams bookingSearchParams);
        Task<bool> IsBookingDoneByUser(Guid userId, Guid bookingId);
        Task<bool> CanOwnerConfirmOrRejectBooking(Guid userId, Guid bookingId);
    }
}
