using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace BookingClone.Application.Features.Notifications.Queries.GetUserNotifications
{
    public class GetUserNotificationsQueryHandler(
        INotificationRepository notificationRepository,
        IPaginationHelper paginationHelper)
        : IRequestHandler<GetUserNotificationsQuery, Result<PagedList<NotificationDTO>>>
    {
        public async Task<Result<PagedList<NotificationDTO>>> Handle(GetUserNotificationsQuery request, CancellationToken cancellationToken)
        {
            if (request.NotificationsSearchParams.UserId == default )
                throw new Exception("User id not provided to get notifications");

            var query = notificationRepository.GetNotificationHistoryForUser(request.NotificationsSearchParams);

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