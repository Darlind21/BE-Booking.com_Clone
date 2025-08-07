using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Helpers
{
    public class PaginationHeader(int currentPage, int pageSize, int totalItems, int totalPages)
    {
        public int CurrentPage { get; set; } = currentPage;
        public int PageSize { get; set; } = pageSize;
        public int TotalItems { get; set; } = totalItems;
        public int TotalPages { get; set; } = totalPages;
    }
}
