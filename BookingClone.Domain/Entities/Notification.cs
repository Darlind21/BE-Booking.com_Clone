using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class Notification
    {
        [Key]
        public Guid Id { get; private set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; private set; }
        public User User { get; private set; } = null!;

        public string Title { get; private set; } = null!;
        public string Message { get; private set; } = null!;
        public bool IsRead { get; private set; }
        public DateTime CreatedOnUtc { get; private init; } = DateTime.UtcNow;
        public DateTime? ReadOnUtc { get; private set; }

        private Notification() { } // For EF Core

        public Notification(Guid userId, string title, string message)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Title = title;
            Message = message;
            IsRead = false;
        }

        public void MarkAsRead()
        {
            if (!IsRead)
            {
                IsRead = true;
                ReadOnUtc = DateTime.UtcNow;
            }
        }
    }
}
