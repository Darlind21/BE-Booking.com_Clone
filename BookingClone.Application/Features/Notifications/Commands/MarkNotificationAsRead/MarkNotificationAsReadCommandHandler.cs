using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace BookingClone.Application.Features.Notifications.Commands.MarkNotificationAsRead
{
    public class MarkNotificationAsReadCommandHandler(INotificationRepository notificationRepository)
        : IRequestHandler<MarkNotificationAsReadCommand, Result>
    {
        public async Task<Result> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await notificationRepository.GetByIdAsync(request.NotificationId);
            if (notification == null || notification.UserId != request.UserId)
                throw new Exception("Notification not found or access denied.");

            if (notification.IsRead)
                return Result.Ok();

            if (await notificationRepository.MarkAsRead(notification.Id)) return Result.Ok();

            else throw new Exception("Unable to mark notification as read at this time");
        }
    }
}