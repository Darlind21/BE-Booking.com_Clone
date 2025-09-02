using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Events.Booking.BookingConfirmed;
using BookingClone.Application.Events.Notifications;
using BookingClone.Application.Features.Booking.Commands.ConfirmBooking;
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

namespace BookingClone.Application.Features.Booking.Commands.ConfirmBooking
{
    public class ConfirmBookingCommandHandler
        (
        IBookingRepository bookingRepository,
        //IPublisher publisher
        IOutboxRepository outboxRepository,
        IJobScheduler jobScheduler,
        INotificationService notificationService,
        INotificationRepository notificationRepository
        )
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


            //await publisher.Publish(new BookingConfirmedEvent
            //{
            //    Booking = booking
            //});



            var outboxMessage = new OutboxMessage(payload: JsonSerializer.Serialize(new EmailPayload
            {
                To = await bookingRepository.GetUserEmailByBookingId(request.BookingId),
                Subject = "Booking Confirmed",
                Body = $"Your booking is confirmed."
            }));

            await outboxRepository.AddAsync(outboxMessage);

            //jobScheduler.Enqueue<IOutboxProcessor>(x => x.ProcessSingleMessage(outboxMessage.Id));
            jobScheduler.EnqueueOutboxMessage(outboxMessage.Id);




            var dbNotification = new Notification(
                booking.UserId,
                "Booking Confirmed",
                "Your booking was confirmed"
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
