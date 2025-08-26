using FluentResults;
using MediatR;

namespace BookingClone.Application.Features.Notifications.Commands.MarkNotificationAsRead
{
    public record MarkNotificationAsReadCommand : IRequest<Result>
    {
        public Guid NotificationId { get; init; }
        public Guid UserId { get; init; }
    }
}