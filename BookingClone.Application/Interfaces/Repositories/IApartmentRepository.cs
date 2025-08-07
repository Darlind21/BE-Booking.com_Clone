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
        Task<decimal?> GetApartmentAverageRating(Guid apartmentId);
        Task<PagedList<Apartment>> SearchAsync(SearchParams searchParams);
    }
}
