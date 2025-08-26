using MediatR;

namespace BookingClone.Application.Events.Booking.BookingCancelled
{
    public record BookingCancelledEvent : INotification
    {
        public Domain.Entities.Booking Booking { get; init; } = default!;
    }
}