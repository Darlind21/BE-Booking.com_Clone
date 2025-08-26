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

namespace BookingClone.Application.Events.Booking.BookingConfirmed
{
    public class BookingConfirmedEventHandler(
        IBookingRepository bookingRepository,
        IOutboxRepository outboxRepository,
        IJobScheduler jobScheduler,
        IMediator mediator)
        : INotificationHandler<BookingConfirmedEvent>
    {
        public async Task Handle(BookingConfirmedEvent notification, CancellationToken cancellationToken)
        {
            await mediator.Publish(new NotificationEvent
            {
                UserId = notification.Booking.UserId,
                Title = "Booking Confirmed",
                Message = $"Your booking has been confirmed."
            }, cancellationToken);



            var outboxMessage = new OutboxMessage(payload: JsonSerializer.Serialize(new EmailPayload
            {
                To = await bookingRepository.GetUserEmailByBookingId(notification.Booking.Id),
                Subject = "Booking Confirmation",
                Body = $"Your booking is confirmed."
            }));

            await outboxRepository.AddAsync(outboxMessage);
            jobScheduler.EnqueueOutboxMessage(outboxMessage.Id);
        }
    }
}