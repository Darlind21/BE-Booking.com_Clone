using BookingClone.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task SendNotificationAsync(string userId, NotificationDTO notification);
        Task SendBulkNotificationAsync(IEnumerable<string> userIds, NotificationDTO notification);
    }
}
