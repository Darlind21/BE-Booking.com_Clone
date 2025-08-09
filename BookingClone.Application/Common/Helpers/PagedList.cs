using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Helpers
{
    public class PagedList<T>(List<T> items, int count, int pageNumber, int pageSize) //: List<T>
    {
        public List<T> Items { get; set; } = items;
        public int TotalCount { get; set; } = count;
        public int PageSize { get; set; } = pageSize;
        public int CurrentPage { get; set; } = pageNumber;
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    }
}
