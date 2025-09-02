using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Features.Booking.Commands.CompleteBooking;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Domain.Entities;
using BookingClone.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.CompleteBookings
{
    public class CompleteBookingsCommandHandler 
        (IBookingRepository bookingRepository,
        IOutboxRepository outboxRepository,
        IJobScheduler jobScheduler,
        INotificationService notificationService,
        INotificationRepository notificationRepository
        //IPublisher publisher
        )
        : IRequestHandler<CompleteBookingsCommand, Result>
    {
        public async Task<Result> Handle(CompleteBookingsCommand request, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null) throw new Exception("Unable to complete booking with this id as it does not exist");

            if (booking.Status != BookingStatus.Confirmed)
                return Result.Fail("Unable to complete booking as its status is not Confirmed");

            if (booking.EndDate > DateOnly.FromDateTime(DateTime.UtcNow))
                return Result.Fail("Cannot complete booking before its end date");

            booking.CompleteBooking();

            var updated = await bookingRepository.UpdateAsync(booking);
            if (!updated) throw new Exception("Unable to complete booking at this time");

            //await publisher.Publish(new BookingCompletedEvent
            //{
            //    Booking = booking
            //});



            var outboxMessage = new OutboxMessage(payload: JsonSerializer.Serialize(new EmailPayload
            {
                To = await bookingRepository.GetUserEmailByBookingId(request.BookingId),
                Subject = "Booking Completed",
                Body = $"Your booking is completed. You can submit a review now!"
            }));

            await outboxRepository.AddAsync(outboxMessage);

            //jobScheduler.Enqueue<IOutboxProcessor>(x => x.ProcessSingleMessage(outboxMessage.Id));
            jobScheduler.EnqueueOutboxMessage(outboxMessage.Id);




            var dbNotification = new Notification(
                booking.UserId,
                "Booking Completed",
                "Your booking was completed. You can submit a review now!"
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
