using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Events.Notifications;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Domain.Entities;
using MediatR;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace BookingClone.Application.Events.Booking.BookingRejected
{
    public class BookingRejectedEventHandler(
        IBookingRepository bookingRepository,
        IOutboxRepository outboxRepository,
        IJobScheduler jobScheduler,
        INotificationRepository notificationRepository,
        INotificationService notificationService)
        : INotificationHandler<BookingRejectedEvent>
    {
        public async Task Handle(BookingRejectedEvent notification, CancellationToken cancellationToken)
        {
            var dbNotification = new Notification(
                notification.Booking.UserId,
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




            var outboxMessage = new OutboxMessage(payload: JsonSerializer.Serialize(new EmailPayload
            {
                To = await bookingRepository.GetUserEmailByBookingId(notification.Booking.Id),
                Subject = "Booking was rejected",
                Body = $"Your booking is rejected."
            }));

            await outboxRepository.AddAsync(outboxMessage);
            jobScheduler.EnqueueOutboxMessage(outboxMessage.Id);
        }
    }
}