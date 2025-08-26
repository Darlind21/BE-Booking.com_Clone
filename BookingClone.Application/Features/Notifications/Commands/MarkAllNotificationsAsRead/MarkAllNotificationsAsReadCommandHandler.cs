using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;

namespace BookingClone.Application.Features.Notifications.Commands.MarkAllNotificationsAsRead
{
    public class MarkAllNotificationsAsReadCommandHandler(INotificationRepository notificationRepository)
        : IRequestHandler<MarkAllNotificationsAsReadCommand, Result>
    {
        public async Task<Result> Handle(MarkAllNotificationsAsReadCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId == default)
                throw new Exception("User id not provided to mark all notifications as read");

            await notificationRepository.MarkAllAsRead(request.UserId);
            return Result.Ok();
        }
    }
}