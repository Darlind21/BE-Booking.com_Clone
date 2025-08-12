using BookingClone.Application.Common.Enums;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Domain.Entities;
using BookingClone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BookingClone.Infrastructure.Repositories
{
    public class BookingRepository(BookingDbContext context) : BaseRepository<Booking>(context), IBookingRepository
    {
        private readonly BookingDbContext context = context;

        public async Task<bool> CanOwnerConfirmOrRejectBooking(Guid userId, Guid bookingId)
        {
            return await context.Bookings
                .AnyAsync(b => b.Id == bookingId && b.Apartment.Owners.Any(o => o.UserId == userId));
        }

        public IQueryable<Booking> GetBookingsByApartmentId(BookingSearchParams bookingSearchParams)
        {
            var query = context.Bookings
                .AsNoTracking()
                .Where(b => b.ApartmentId == bookingSearchParams.ApartmentId!.Value)
                .Include(b => b.Apartment)
                .AsQueryable();

            if (bookingSearchParams.Status.HasValue)
            {
                query = query.Where(b => b.Status == bookingSearchParams.Status.Value);
            }

            if (bookingSearchParams.FromDate.HasValue)
            {
                query = query.Where(b => b.StartDate >= bookingSearchParams.FromDate.Value);
            }

            if (bookingSearchParams.ToDate.HasValue)
            {
                query = query.Where(b => b.EndDate <= bookingSearchParams.ToDate.Value);
            }

            switch(bookingSearchParams.SortBy)
            {
                case BookingSortBy.CreatedAt:
                    query = bookingSearchParams.SortDescending ? query.OrderByDescending(b => b.CreatedOnUtc) : query.OrderBy(b => b.CreatedOnUtc);
                    break;

                default:
                    query = bookingSearchParams.SortDescending ? query.OrderByDescending(b => b.CreatedOnUtc) : query.OrderBy(b => b.CreatedOnUtc);
                    break;
            };

            return query;
        }

        public IQueryable<Booking> GetBookingsByUserId(BookingSearchParams bookingSearchParams)
        {
            var query = context.Bookings
                .AsNoTracking()
                .Where(b => b.UserId == bookingSearchParams.UserId!.Value) //will throw if userid is null
                .Include(b => b.Apartment)
                .AsQueryable();

            if (bookingSearchParams.Status.HasValue)
            {
                query = query.Where(b => b.Status == bookingSearchParams.Status.Value);
            }

            if (bookingSearchParams.FromDate.HasValue)
            {
                query = query.Where(b => b.StartDate >= bookingSearchParams.FromDate.Value);
            }

            if (bookingSearchParams.ToDate.HasValue)
            {
                query = query.Where(b => b.EndDate <= bookingSearchParams.ToDate.Value);
            }

            switch (bookingSearchParams.SortBy)
            {
                case BookingSortBy.CreatedAt:
                    query = bookingSearchParams.SortDescending ? query.OrderByDescending(b => b.CreatedOnUtc) : query.OrderBy(b => b.CreatedOnUtc);
                    break;

                default:
                    query = bookingSearchParams.SortDescending ? query.OrderByDescending(b => b.CreatedOnUtc) : query.OrderBy(b => b.CreatedOnUtc);
                    break;
            }
            ;

            return query;
        }

        public async Task<bool> IsBookingDoneByUser(Guid userId, Guid bookingId)
        {
            return await context.Bookings
                .AnyAsync(b => b.Id == bookingId && b.UserId == userId);
        }
    }
}
