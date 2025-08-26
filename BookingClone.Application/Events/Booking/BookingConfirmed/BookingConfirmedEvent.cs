using MediatR;
using System;

namespace BookingClone.Application.Events.Booking.BookingConfirmed
{
    public record BookingConfirmedEvent : INotification
    {
        public Domain.Entities.Booking Booking { get; init; } = default!;
    }
}