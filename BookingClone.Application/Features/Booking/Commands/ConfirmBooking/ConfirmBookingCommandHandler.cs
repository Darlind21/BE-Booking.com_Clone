using BookingClone.Application.Features.Booking.Commands.ApproveBooking;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.ConfirmBooking
{
    public class ConfirmBookingCommandHandler
        (IBookingRepository bookingRepository)
        : IRequestHandler<ConfirmBookingCommand, Result>
    {
        public async Task<Result> Handle(ConfirmBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null) throw new Exception("Booking with this id does not exist");

            if (!await bookingRepository.CanOwnerConfirmOrRejectBooking(request.UserId, request.BookingId))
                throw new Exception("Owner with this userid cannot confirm booking since he is not an owner of the apartment for this booking");

            if (booking.Status == Domain.Enums.BookingStatus.Rejected)
                throw new Exception("Unable to confirm booking as it is rejected");

            if (booking.Status == Domain.Enums.BookingStatus.Cancelled)
                throw new Exception("Unable to confirm booking as it is cancelled"); 

            if (booking.Status == Domain.Enums.BookingStatus.Confirmed)
                throw new Exception("Unable to confirm booking as it is already confirmed"); 

            if (booking.Status == Domain.Enums.BookingStatus.Completed)
                throw new Exception("Unable to confirm booking as it is completed"); 

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (booking.StartDate <= today || booking.EndDate <= today)
                throw new Exception("Unable to confirm booking as it has already started or has ended");

            booking.ConfirmBooking();

            var updated = await bookingRepository.UpdateAsync(booking);
            if (!updated) throw new Exception("Unable to confirm booking at this time");

            return Result.Ok();
        }
    }
}
