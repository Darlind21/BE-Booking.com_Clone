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

namespace BookingClone.Application.Events.Booking.BookingCancelled
{
    public class BookingCancelledEventHandler(
        IBookingRepository bookingRepository,
        IOutboxRepository outboxRepository,
        IJobScheduler jobScheduler,
        IMediator mediator)
        : INotificationHandler<BookingCancelledEvent>
    {
        public async Task Handle(BookingCancelledEvent notification, CancellationToken cancellationToken)
        {
            await mediator.Publish(new NotificationEvent
            {
                UserId = notification.Booking.UserId,
                Title = "Booking Cancelled",
                Message = $"Your booking has been cancelled."
            }, cancellationToken);

            var outboxMessage = new OutboxMessage(payload: JsonSerializer.Serialize(new EmailPayload
            {
                To = await bookingRepository.GetUserEmailByBookingId(notification.Booking.Id),
                Subject = "Booking Cancelled",
                Body = $"Your booking was cancelled."
            }));

            await outboxRepository.AddAsync(outboxMessage);
            jobScheduler.EnqueueOutboxMessage(outboxMessage.Id);
        }
    }
}