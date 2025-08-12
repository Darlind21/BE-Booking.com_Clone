using BookingClone.Application.Features.Booking.Commands.CompleteBooking;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.CompleteBookings
{
    public class CompleteBookingsCommandHandler 
        (IBookingRepository bookingRepository)
        : IRequestHandler<CompleteBookingsCommand, Result>
    {
        public async Task<Result> Handle(CompleteBookingsCommand request, CancellationToken cancellationToken)
        {
            if (request.BookingId.HasValue)
            {
                var booking = await bookingRepository.GetByIdAsync(request.BookingId.Value);
                if (booking == null) throw new Exception("Unable to complete booking with this id as it does not exist");

                booking.CompleteBooking();

                var updated = await bookingRepository.UpdateAsync(booking);
                if (!updated) throw new Exception("Unable to complete booking at this time");

                return Result.Ok();
            }


            var expiredBookings = bookingRepository
                .GetExpiredBookingsQuery()
                .ToList();

            if (!expiredBookings.Any())
                return Result.Ok(); //if there are no expired booking there is nothing to do 

            foreach (var booking in expiredBookings)
            {
                booking.CompleteBooking();
            }

            var saved = await bookingRepository.SaveChangesAsync();
            if (!saved) throw new Exception("Unable to auto complete bookings");

            return Result.Ok();
        }
    }
}
