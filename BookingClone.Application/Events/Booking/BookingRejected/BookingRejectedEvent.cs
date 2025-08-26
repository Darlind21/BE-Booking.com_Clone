using MediatR;

namespace BookingClone.Application.Events.Booking.BookingRejected
{
    public record BookingRejectedEvent : INotification
    {
        public Domain.Entities.Booking Booking { get; init; } = default!;
    }
}