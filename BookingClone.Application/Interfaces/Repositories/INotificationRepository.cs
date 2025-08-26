using BookingClone.Application.Common.Helpers;
using BookingClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Repositories
{
    public interface INotificationRepository : IBaseRepository<Notification>
    {
        IQueryable<Notification> GetUnreadNotificationsForUser(NotificationsSearchParams notificationsSearchParams);
        IQueryable<Notification> GetNotificationHistoryForUser(NotificationsSearchParams notificationsSearchParams);
        Task<bool> MarkAsRead(Guid notificationId);
        Task MarkAllAsRead(Guid userId);
    }
}
