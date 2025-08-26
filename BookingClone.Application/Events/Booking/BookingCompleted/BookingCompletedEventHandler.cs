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
        IMediator mediator)
        : INotificationHandler<BookingCompletedEvent>
    {
        public async Task Handle(BookingCompletedEvent notification, CancellationToken cancellationToken)
        {
            await mediator.Publish(new NotificationEvent
            {
                UserId = notification.Booking.UserId,
                Title = "Booking Completed",
                Message = $"Your booking has been marked as completed."
            }, cancellationToken);

            var outboxMessage = new OutboxMessage(payload: JsonSerializer.Serialize(new EmailPayload
            {
                To = await bookingRepository.GetUserEmailByBookingId(notification.Booking.Id),
                Subject = "Booking Completed",
                Body = $"Your booking has been completed."
            }));

            await outboxRepository.AddAsync(outboxMessage);
            jobScheduler.EnqueueOutboxMessage(outboxMessage.Id);
        }
    }
}