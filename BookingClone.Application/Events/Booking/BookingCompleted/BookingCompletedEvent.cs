using MediatR;

namespace BookingClone.Application.Events.Booking.BookingCompleted
{
    public record BookingCompletedEvent : INotification
    {
        public Domain.Entities.Booking Booking { get; init; } = default!;
    }
}