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
    public class BookingRepository(BookingDbContext context) : BaseRepository<Booking>(context), IBookingRepository
    {
        private readonly BookingDbContext context = context;


        public async Task<List<Booking>> GetBookingsByUserId(Guid userId)
        {
            return await context.Bookings
                .Where(b => b.UserId == userId)
                .Include(b => b.Apartment)
                .ToListAsync();
        }
    }
}
