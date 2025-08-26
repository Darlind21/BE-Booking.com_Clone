using BookingClone.Application.Common.Enums;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Domain.Entities;
using BookingClone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Repositories
{
    public class NotificationRepository(BookingDbContext context) : BaseRepository<Notification>(context), INotificationRepository
    {
        private readonly BookingDbContext context = context;


        public async Task<bool> MarkAsRead(Guid notificationId)
        {
            var notification = await context.Notifications.FindAsync(notificationId);
            if (notification != null && !notification.IsRead)
            {
                notification.MarkAsRead();
            }
            return await context.SaveChangesAsync() > 0;
        }

        public async Task MarkAllAsRead(Guid userId)
        {
            var notifications = await context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.MarkAsRead();
            }

            if (notifications.Count > 0)
                await context.SaveChangesAsync();
        }

        public IQueryable<Notification> GetUnreadNotificationsForUser(NotificationsSearchParams notificationsSearchParams)
        {
            var query = context.Notifications
                .AsNoTracking()
                .Where(n => n.UserId == notificationsSearchParams.UserId && !n.IsRead)
                .AsQueryable();

            if (notificationsSearchParams.FromDate.HasValue)
            {
                query = query.Where(n => n.CreatedOnUtc.Date >= notificationsSearchParams.FromDate.Value);
            }

            if (notificationsSearchParams.ToDate.HasValue)
            {
                query = query.Where(n => n.CreatedOnUtc <= notificationsSearchParams.ToDate.Value);
            }

            switch (notificationsSearchParams.SortBy)
            {
                default:
                    query = notificationsSearchParams.SortDescending ? query.OrderByDescending(n => n.CreatedOnUtc) : query.OrderBy(b => b.CreatedOnUtc);
                    break;
            };

            return query;
        }

        public IQueryable<Notification> GetNotificationHistoryForUser(NotificationsSearchParams notificationsSearchParams)
        {
            var query = context.Notifications
                .AsNoTracking()
                .Where(n => n.UserId == notificationsSearchParams.UserId)
                .AsQueryable();

            if (notificationsSearchParams.FromDate.HasValue)
            {
                query = query.Where(n => n.CreatedOnUtc.Date >= notificationsSearchParams.FromDate.Value);
            }

            if (notificationsSearchParams.ToDate.HasValue)
            {
                query = query.Where(n => n.CreatedOnUtc <= notificationsSearchParams.ToDate.Value);
            }

            switch (notificationsSearchParams.SortBy)
            {
                default:
                    query = notificationsSearchParams.SortDescending ? query.OrderByDescending(n => n.CreatedOnUtc) : query.OrderBy(b => b.CreatedOnUtc);
                    break;
            }
            ;

            return query;
        }
    }
}
