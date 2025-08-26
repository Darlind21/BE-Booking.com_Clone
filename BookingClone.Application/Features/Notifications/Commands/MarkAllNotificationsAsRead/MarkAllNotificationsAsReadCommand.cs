using FluentResults;
using MediatR;

namespace BookingClone.Application.Features.Notifications.Commands.MarkAllNotificationsAsRead
{
    public record MarkAllNotificationsAsReadCommand : IRequest<Result>
    {
        public Guid UserId { get; init; }
    }
}