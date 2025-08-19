using BookingClone.Application.Features.Booking.Commands.AutocompleteExpiredBookings;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Jobs
{
    public class AutocompleteBookingsJob(ISender sender, ILogger<AutocompleteBookingsJob> logger)
    {
        public async Task CompleteBookingsAsync()
        {
            await sender.Send(new AutocompleteExpiredBookingsCommand());
            logger.LogInformation($"Booking auto-completion job completed successfully");
        }
    }
}
