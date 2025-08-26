using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using FluentResults;
using MediatR;

namespace BookingClone.Application.Features.Notifications.Queries.GetUserNotifications
{
    public record GetUserNotificationsQuery : IRequest<Result<PagedList<NotificationDTO>>>
    {
        public NotificationsSearchParams NotificationsSearchParams { get; init; } = default!;
    }
}