using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Enums
{
    public enum BookingStatus
    {
        [Display(Name = "Pending")]
        Pending = 1,

        [Display(Name = "Confirmed")]
        Confirmed = 2,

        [Display(Name = "Rejected")]
        Rejected = 3,

        [Display(Name = "Cancelled")]
        Cancelled = 4,

        [Display(Name = "Completed")]
        Completed = 5,
    }
}
