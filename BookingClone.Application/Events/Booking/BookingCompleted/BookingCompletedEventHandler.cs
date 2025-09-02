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

namespace BookingClone.Application.Events.Booking.BookingCompleted
{
    public class BookingCompletedEventHandler(
        IBookingRepository bookingRepository,
        IOutboxRepository outboxRepository,
        IJobScheduler jobScheduler,
        INotificationRepository notificationRepository,
        INotificationService notificationService)
        : INotificationHandler<BookingCompletedEvent>
    {
        public async Task Handle(BookingCompletedEvent notification, CancellationToken cancellationToken)
        {
            var dbNotification = new Notification(
                notification.Booking.UserId,
                "Booking Completed",
                "Your booking was completed. Feel free to leave a review!"
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
                Subject = "Booking Completed",
                Body = $"Your booking is completed. Feel free to leave a review!"
            }));

            await outboxRepository.AddAsync(outboxMessage);
            jobScheduler.EnqueueOutboxMessage(outboxMessage.Id);
        }
    }
}