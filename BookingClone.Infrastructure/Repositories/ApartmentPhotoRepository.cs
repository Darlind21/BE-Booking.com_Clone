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
    public class ApartmentPhotoRepository(BookingDbContext context) : BaseRepository<ApartmentPhoto>(context), IApartmentPhotoRepository
    {
        private readonly BookingDbContext context = context;
        public async Task<List<ApartmentPhoto>> GetPhotosByApartmentId(Guid apartmentId)
        {
            if (await context.Apartments.FindAsync(apartmentId) == null) throw new Exception("Apartment with this id does not exist");

            return await context.ApartmentPhotos
                .Where(ap => ap.ApartmentId == apartmentId)
                .ToListAsync();
        }

        public async Task<int> GetPhotosCountForApartment(Guid apartmentId)
        {
            return await context.ApartmentPhotos
                .Where(ap => ap.ApartmentId == apartmentId)
                .CountAsync();
        }
    }
}
