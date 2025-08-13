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
    public class ReviewRepository(BookingDbContext context) : BaseRepository<Review>(context), IReviewRepository
    {
        private readonly BookingDbContext context = context;

        public IQueryable<Review> GetReviewsForApartment(ReviewSearchParams reviewsSearchParams)
        {
            var query = context.Reviews
                .AsNoTracking()
                .Where(r => r.Booking.Apartment.Id == reviewsSearchParams.ApartmentId);

            if (reviewsSearchParams.FromDate.HasValue)
            {
                var fromDateTime = reviewsSearchParams.FromDate.Value.ToDateTime(TimeOnly.MinValue);
                query = query.Where(r => r.CreatedOnUtc >= fromDateTime);
            }

            if (reviewsSearchParams.ToDate.HasValue)
            {
                var toDateTime = reviewsSearchParams.ToDate.Value.ToDateTime(TimeOnly.MaxValue);
                query = query.Where(r => r.CreatedOnUtc <= toDateTime);
            }

            if (reviewsSearchParams.MinRating > 0)
                query = query.Where(r => r.RatingOutOfTen >= reviewsSearchParams.MinRating);

            if (reviewsSearchParams.MaxRating > 0)
                query = query.Where(r => r.RatingOutOfTen <= reviewsSearchParams.MaxRating);

            query = reviewsSearchParams.SortBy switch
            {
                ReviewSortBy.CreatedOn => reviewsSearchParams.SortDescending
                    ? query.OrderByDescending(r => r.CreatedOnUtc)
                    : query.OrderBy(r => r.CreatedOnUtc),

                ReviewSortBy.Rating => reviewsSearchParams.SortDescending
                    ? query.OrderByDescending(r => r.RatingOutOfTen)
                    : query.OrderBy(r => r.RatingOutOfTen),

                _ => query
            };

            return query;
        }

        public IQueryable<Review> GetReviewsForUser(ReviewSearchParams reviewsSearchParams, Guid userId)
        {
            var query = context.Reviews
                .AsNoTracking()
                .Where(r => r.UserId == userId);

            if (reviewsSearchParams.FromDate.HasValue)
            {
                var fromDateTime = reviewsSearchParams.FromDate.Value.ToDateTime(TimeOnly.MinValue);
                query = query.Where(r => r.CreatedOnUtc >= fromDateTime);
            }

            if (reviewsSearchParams.ToDate.HasValue)
            {
                var toDateTime = reviewsSearchParams.ToDate.Value.ToDateTime(TimeOnly.MaxValue);
                query = query.Where(r => r.CreatedOnUtc <= toDateTime);
            }

            if (reviewsSearchParams.MinRating > 0)
                query = query.Where(r => r.RatingOutOfTen >= reviewsSearchParams.MinRating);

            if (reviewsSearchParams.MaxRating > 0)
                query = query.Where(r => r.RatingOutOfTen <= reviewsSearchParams.MaxRating);

            query = reviewsSearchParams.SortBy switch
            {
                ReviewSortBy.CreatedOn => reviewsSearchParams.SortDescending
                    ? query.OrderByDescending(r => r.CreatedOnUtc)
                    : query.OrderBy(r => r.CreatedOnUtc),

                ReviewSortBy.Rating => reviewsSearchParams.SortDescending
                    ? query.OrderByDescending(r => r.RatingOutOfTen)
                    : query.OrderBy(r => r.RatingOutOfTen),

                _ => query
            };

            return query;
        }
    }
}
