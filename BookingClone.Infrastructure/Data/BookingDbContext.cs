using BookingClone.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Data
{
    public class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext (options)
    {
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<ApartmentPhoto> ApartmentPhotos { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Owner> Owners { get; set; }
    }
}
