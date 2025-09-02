using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.CancelBooking
{
    public class CancelBookingCommandHandler
        (IBookingRepository bookingRepository,
        IOutboxRepository outboxRepository,
        IJobScheduler jobScheduler,
        INotificationService notificationService,
        INotificationRepository notificationRepository
        //IPublisher publisher
        )
        : IRequestHandler<CancelBookingCommand, Result>
    {
        public async Task<Result> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null) throw new Exception("Booking with this id does not exist");

            if (!await bookingRepository.IsBookingDoneByUser(request.UserId, request.BookingId))
                throw new Exception("This booking is not done by a user with this id");

            if (booking.Status == Domain.Enums.BookingStatus.Rejected)
                throw new Exception("Unable to cancel booking as it is rejected");

            if (booking.Status == Domain.Enums.BookingStatus.Cancelled)
                throw new Exception("Booking is already cancelled"); //we throw exception because user is not even supposed to send a request to cancel if its already cancelled

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (booking.StartDate <= today || booking.EndDate <= today) 
                throw new Exception("Unable to cancel booking as it has already started or has ended");
            
            booking.CancelBooking();

            var updated = await bookingRepository.UpdateAsync(booking);
            if (!updated) throw new Exception("Unable to cancel booking at this time");



            //await publisher.Publish(new BookingCancelledEvent
            //{
            //    Booking = booking
            //});



            var outboxMessage = new OutboxMessage(payload: JsonSerializer.Serialize(new EmailPayload
            {
                To = await bookingRepository.GetUserEmailByBookingId(request.BookingId),
                Subject = "Booking Cancelled",
                Body = $"Your booking is cancelled."
            }));

            await outboxRepository.AddAsync(outboxMessage);

            //jobScheduler.Enqueue<IOutboxProcessor>(x => x.ProcessSingleMessage(outboxMessage.Id));
            jobScheduler.EnqueueOutboxMessage(outboxMessage.Id);




            var dbNotification = new Notification(
                booking.UserId,
                "Booking Cancelled",
                "Your booking was cancelled"
            );

            await notificationRepository.AddAsync(dbNotification);

            var notificationDto = new NotificationDTO
            {
                Id = dbNotification.Id,
                Title = dbNotification.Title,
                Message = dbNotification.Message,
                IsRead = false,
                CreatedOnUtc = dbNotification.CreatedOnUtc
            };

            await notificationService.SendNotificationAsync(
                dbNotification.UserId.ToString(),
                notificationDto
            );

            return Result.Ok();
        }
    }
}
