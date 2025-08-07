using BookingClone.Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Interfaces
{
    public interface IPaginationHelper
    {
        Task<PagedList<T>> PaginateAsync<T>(IQueryable<T> source, int pageNumber, int pageSize);
    }
}
