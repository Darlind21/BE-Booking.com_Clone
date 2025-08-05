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
    public class ApartmentPhotoRepository(BookingDbContext context) : BaseRepository<ApartmentPhoto>(context), IApartmentPhotoRepository
    {
    }
}
