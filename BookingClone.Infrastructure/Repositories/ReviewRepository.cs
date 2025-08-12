using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Domain.Entities;
using BookingClone.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Repositories
{
    public class ReviewRepository(BookingDbContext context) : BaseRepository<Review>(context), IReviewRepository
    {
        private readonly BookingDbContext context = context;
    }
}
