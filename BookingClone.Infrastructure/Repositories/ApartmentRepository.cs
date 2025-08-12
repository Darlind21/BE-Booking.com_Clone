using BookingClone.Application.Common.Enums;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Domain.Entities;
using BookingClone.Domain.Enums;
using BookingClone.Infrastructure.Data;
using BookingClone.Infrastructure.Helpers;
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

        public async Task<bool> ApartmentBelongsToOwner(Guid apartmentId, Guid userId)
        {
            return await context.Apartments
                .AsNoTracking()
                .AnyAsync(a => a.Id == apartmentId && a.Owners.Any(o => o.UserId == userId));
        }

        public async Task<List<Amenity>> GetAmenitiesForApartment(Guid apartmentId)
        {
            var apt = await context.Apartments
                .Include(a => a.Amenities)
                .FirstOrDefaultAsync(a => a.Id == apartmentId);

            return apt?.Amenities?.ToList() ?? new List<Amenity>();
        }

        public async Task<decimal?> GetApartmentAverageRatingAsync(Guid apartmentId)
        {
            return await context.Reviews
                .AsNoTracking()
                .Where(r => r.Booking.ApartmentId == apartmentId)
                .Select(r => (decimal?)r.RatingOutOfTen) //if we use non-nullable decimal ef core will throw since there are no values to agerage
                .AverageAsync(); //returns null if no reviews since we are using decimal?

        }

        public async Task<bool> IsApartmentAvailable(Guid apartmentId, DateOnly checkinDate, DateOnly checkoutDate)
        {
            var hasOverlappingBookings = await context.Bookings.AnyAsync(b =>
                b.ApartmentId == apartmentId &&
                b.Status == BookingStatus.Confirmed &&
                !(checkoutDate <= b.StartDate || checkinDate >= b.EndDate)
            );

            return !hasOverlappingBookings;
        }

        public IQueryable<Apartment> Search(ApartmentSearchParams searchParams)
        {
            var query = context.Apartments
                //.Include(a => a.ApartmentPhotos)
                //.Include(a => a.Bookings)
                //    .ThenInclude(b => b.Review)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchParams.Name)) query = query.Where(a => a.Name.ToLower().Contains(searchParams.Name.ToLower()));

            if (!string.IsNullOrEmpty(searchParams.Address)) query = query.Where(a => a.Address.ToLower().Contains(searchParams.Address.ToLower()));

            if (searchParams.MinPrice != null) query = query.Where(a => a.PricePerDay >= searchParams.MinPrice);

            if (searchParams.MaxPrice != null) query = query.Where(a => a.PricePerDay <= searchParams.MaxPrice);


            query = query
                .Where(a => !a.Bookings.Any(b =>
                    b.Status == BookingStatus.Confirmed &&
                    !(searchParams.CheckoutDate <= b.StartDate || searchParams.CheckInDate >= b.EndDate) //condition inside () returns true when apt is available  
                ));


            switch (searchParams.SortBy)
            {
                case ApartmentSortBy.Price:
                    query = searchParams.SortDescending ? query.OrderByDescending(a => a.PricePerDay) : query.OrderBy(a => a.PricePerDay);
                    break;

                case ApartmentSortBy.Name:
                    query = searchParams.SortDescending ? query.OrderByDescending(a => a.Name) : query.OrderBy(a => a.Name);
                    break;

                case ApartmentSortBy.AverageRating:
                    if (searchParams.SortDescending == true)
                    {
                        query = query.OrderByDescending(a => 
                            a.Bookings.Any(b => b.Review != null) //check if this apartment (a) has any bookings (b) that have a review (!= null)
                            ? a.Bookings.Where(b => b.Review != null).Average(b => b.Review!.RatingOutOfTen) //if yes get all bookings (b) that have a review and then calculate average rating 
                            : 0); // if apt (a) has no bookings or no bookings with reviews we use 0 as default rating
                    }
                    else
                    {
                        query = query.OrderBy(a =>
                            a.Bookings.Any(b => b.Review != null)
                                ? a.Bookings.Where(b => b.Review != null).Average(b => b.Review!.RatingOutOfTen)
                                : 0);
                    }
                    break;

                case ApartmentSortBy.Popularity:
                    query = searchParams.SortDescending ? query.OrderByDescending(a => a.Bookings.Count) : query.OrderBy(a => a.Bookings.Count);
                    break;

                default:
                    query = query.OrderBy(a => a.Bookings.Count);
                    break;
            }


            return query; //returning query to handle pagination in the handlerS
        }
    }
}
