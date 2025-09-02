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

namespace BookingClone.Application.Features.Booking.Commands.RejectBooking
{
    public class RejectBookingCommandHandle
        (IBookingRepository bookingRepository,
        IOutboxRepository outboxRepository,
        IJobScheduler jobScheduler,
        INotificationService notificationService,
        INotificationRepository notificationRepository
        //IPublisher publisher
        )
        : IRequestHandler<RejectBookingCommand, Result>
    {
        public async Task<Result> Handle(RejectBookingCommand request, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null) throw new Exception("Booking with this id does not exist");

            if (!await bookingRepository.CanOwnerConfirmOrRejectBooking(request.UserId, request.BookingId))
                throw new Exception("Owner with this userid cannot reject booking since he is not an owner of the apartment for this booking");

            if (booking.Status == Domain.Enums.BookingStatus.Rejected)
                throw new Exception("Unable to reject booking as it is already rejected");

            if (booking.Status == Domain.Enums.BookingStatus.Cancelled)
                throw new Exception("Unable to reject booking as it is cancelled");

            if (booking.Status == Domain.Enums.BookingStatus.Confirmed)
                throw new Exception("Unable to reject booking as it is confirmed");

            if (booking.Status == Domain.Enums.BookingStatus.Completed)
                throw new Exception("Unable to reject booking as it is completed");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (booking.StartDate <= today || booking.EndDate <= today)
                throw new Exception("Unable to reject booking as it has already started or has ended");

            booking.RejectBooking();

            var updated = await bookingRepository.UpdateAsync(booking);
            if (!updated) throw new Exception("Unable to reject booking at this time");

            //await publisher.Publish(new BookingRejectedEvent
            //{
            //    Booking = booking
            //});



            var outboxMessage = new OutboxMessage(payload: JsonSerializer.Serialize(new EmailPayload
            {
                To = await bookingRepository.GetUserEmailByBookingId(request.BookingId),
                Subject = "Booking Rejected",
                Body = $"Your booking is rejected."
            }));

            await outboxRepository.AddAsync(outboxMessage);

            //jobScheduler.Enqueue<IOutboxProcessor>(x => x.ProcessSingleMessage(outboxMessage.Id));
            jobScheduler.EnqueueOutboxMessage(outboxMessage.Id);




            var dbNotification = new Notification(
                booking.UserId,
                "Booking Rejected",
                "Your booking was rejected"
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
