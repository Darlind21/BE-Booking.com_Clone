using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace BookingClone.Application.Features.Notifications.Queries.GetUnreadNotifications
{
    public class GetUnreadNotificationsQueryHandler
        (INotificationRepository notificationRepository,
        IPaginationHelper paginationHelper)
        : IRequestHandler<GetUnreadNotificationsQuery, Result<PagedList<NotificationDTO>>>
    {
        public async Task<Result<PagedList<NotificationDTO>>> Handle(GetUnreadNotificationsQuery request, CancellationToken cancellationToken)
        {
            if (request.NotificationsSearchParams.UserId == default)
                throw new Exception("User id not provided to get notifications");

            var query = notificationRepository.GetUnreadNotificationsForUser(request.NotificationsSearchParams);

            var projected = query.Select(n => new NotificationDTO
            {
                Id = n.Id,
                Title = n.Title,
                Message = n.Message,
                IsRead = n.IsRead,
                CreatedOnUtc = n.CreatedOnUtc,
                ReadOnUtc = n.ReadOnUtc
            });

            return await paginationHelper.PaginateAsync(projected, request.NotificationsSearchParams.PageNumber, request.NotificationsSearchParams.PageSize);
        }
    }
}