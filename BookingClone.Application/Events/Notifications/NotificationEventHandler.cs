using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Events.Notifications
{
    public class NotificationEventHandler(
        INotificationService notificationService,
        INotificationRepository notificationRepository) : INotificationHandler<NotificationEvent>
    {
        private readonly INotificationService _notificationService = notificationService;
        private readonly INotificationRepository _notificationRepository = notificationRepository;

        public async Task Handle(NotificationEvent notification, CancellationToken cancellationToken)
        {
            var dbNotification = new Notification(
                notification.UserId,
                notification.Title,
                notification.Message
            );

            await _notificationRepository.AddAsync(dbNotification);

            var notificationDto = new NotificationDTO
            {
                Id = dbNotification.Id,
                Title = notification.Title,
                Message = notification.Message,
                IsRead = false,
                CreatedOnUtc = dbNotification.CreatedOnUtc
            };

            await _notificationService.SendNotificationAsync(
                notification.UserId.ToString(),
                notificationDto
            );
        }
    }
}
