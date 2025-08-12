using BookingClone.Application.Common.Helpers;
using BookingClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Repositories
{
    public interface IApartmentRepository : IBaseRepository<Apartment>
    {
        Task<List<Amenity>> GetAmenitiesForApartment(Guid apartmentId);
        Task<decimal?> GetApartmentAverageRatingAsync(Guid apartmentId);
        IQueryable<Apartment> Search(ApartmentSearchParams searchParams);
        Task<bool> IsApartmentAvailable(Guid apartmentId, DateOnly checkinDate, DateOnly checkoutDate);
    }
}
