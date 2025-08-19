using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.AutocompleteExpiredBookings
{
    public class AutocompleteExpiredBookingsCommandHandler(IBookingRepository bookingRepository)
        : IRequestHandler<AutocompleteExpiredBookingsCommand>
    {
        public async Task Handle(AutocompleteExpiredBookingsCommand request, CancellationToken cancellationToken)
        {
            var expiredBookings = bookingRepository
                .GetExpiredBookingsQuery()
                .ToList();

            if (!expiredBookings.Any())
                return; //if there are no expired booking there is nothing to do 

            foreach (var booking in expiredBookings)
            {
                booking.CompleteBooking();
            }

            var saved = await bookingRepository.SaveChangesAsync();
            if (!saved) throw new Exception("Unable to auto complete bookings");

            return;
        }
    }
}
