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

namespace BookingClone.Infrastructure.Repositories
{
    public class ApartmentRepository(BookingDbContext context) : BaseRepository<Apartment>(context), IApartmentRepository
    {
        private readonly BookingDbContext context = context;
        public async Task<List<Amenity>> GetAmenitiesForApartment(Guid apartmentId)
        {
            var apt = await context.Apartments
                .Include(a => a.Amenities)
                .FirstOrDefaultAsync(a => a.Id == apartmentId);

            return apt?.Amenities?.ToList() ?? new List<Amenity>();
        }

        public async Task<decimal?> GetApartmentAverageRating(Guid apartmentId)
        {
            return await context.Reviews
                .AsNoTracking()
                .Where(r => r.Booking.ApartmentId == apartmentId)
                .Select(r => (decimal?)r.RatingOutOfTen) //if we use non-nullable decimal ef core will throw since there are no values to agerage
                .AverageAsync(); //returns null if no reviews since we are using decimal?

        }

        public Task<PagedList<Apartment>> SearchAsync(SearchParams searchParams)
        {
            throw new NotImplementedException();
        }
    }
}
