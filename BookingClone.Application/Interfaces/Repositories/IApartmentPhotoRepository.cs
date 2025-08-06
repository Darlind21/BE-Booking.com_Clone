using BookingClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Repositories
{
    public interface IApartmentPhotoRepository : IBaseRepository<ApartmentPhoto>
    {
        Task<List<ApartmentPhoto>> GetPhotosByApartmentId(Guid apartmentId);
        Task<int> GetPhotosCountForApartment(Guid apartmentId);

    }
}
