using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Events.Notifications
{
    public record NotificationEvent : INotification
    {
        public Guid UserId { get; init; }
        public string Title { get; init; } = null!;
        public string Message { get; init; } = null!;
    }
}
