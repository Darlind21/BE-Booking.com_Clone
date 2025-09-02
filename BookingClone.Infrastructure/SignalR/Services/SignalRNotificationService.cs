using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Infrastructure.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.SignalR.Services
{
    public class SignalRNotificationService(IHubContext<NotificationHub> hubContext) : INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext = hubContext;
        //IHubContext<NotificationHub> is a long-lived entry point that lets backgroud services, controllers, event handlers, etc,
        //talk to all connected clients(or groups) without having to be inside a hub instance 

        public async Task SendNotificationAsync(string userId, NotificationDTO notification)
        {
            await _hubContext.Clients
                .Group($"User_{userId}")
                .SendAsync("ReceiveNotification", notification);
        }

        public async Task SendBulkNotificationAsync(IEnumerable<string> userIds, NotificationDTO notification)
        {
            var tasks = userIds.Select(userId => 
                _hubContext.Clients
                    .Group($"User_{userId}")
                    .SendAsync("ReceiveNotification", notification));

            await Task.WhenAll(tasks);
        }
    }
}
