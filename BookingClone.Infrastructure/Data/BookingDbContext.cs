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
        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }


        //to apply our configuration classes that implement IEntityTypeConfiguration<T> we can do it on OnModelCreating()
        //we can either apply them all at once using "modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);"
        //or one by one using "modelBuilder.ApplyConfiguration(new TConfiguration());"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //The line below tells EF Core "Scan the assembly where DbContext lives (because thats also where my configuration classes live) and apply all classes that implement IEntityTypeConfiguration<T>"
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly); //since we do not use an instance of BookingDbContext we use typeof to get its type.
            //1.Uses typeof(DbContext) to find the type
            //2.Uses.Assemnly to ge the assembly(DLL) where that type lives -- Assembly is the compiled version of the project the code lives on
            //3.Scans that assembly for all entity configurations and applies them 

            //typeof - C# keyword that gets the Type object (blueprint info) for a class 
            //i.e. Type t = typeof(string);
            //Console.WriteLine(t.FullName); prints System.String
        }

    }
}
