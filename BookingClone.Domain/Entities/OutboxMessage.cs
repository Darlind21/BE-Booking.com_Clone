using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Domain.Entities
{
    public class OutboxMessage
    {
        public Guid Id { get; private set; }
        public DateTime OccurredOnUtc { get; private set; }
        public string Payload { get; private set; } = null!;
        public DateTime? ProcessedOnUtc { get; private set; }
        public string? Error { get; private set; }
        public int RetryCount { get; private set; }
        public int MaxRetries { get; private set; }
        public DateTime? LastAttemptUtc { get; private set; }


        public OutboxMessage(){}


        public OutboxMessage(
            string payload,
            int maxRetries = 5)
        {
            Id = Guid.NewGuid();
            OccurredOnUtc = DateTime.UtcNow;
            Payload = payload;
            RetryCount = 0;
            MaxRetries = maxRetries;
        }

        public void MarkProcessed()
        {
            ProcessedOnUtc = DateTime.UtcNow;
            Error = null;
        }

        public void SetError(string error)
        {
            Error = error;
            LastAttemptUtc = DateTime.UtcNow;
            RetryCount++;
        }

        public void UpdatePayload(string payload)
        {
            Payload = payload;
        }

        public void SetMaxRetries(int maxRetries)
        {
            MaxRetries = maxRetries;
        }

        public void UpdateLastAttempt()
        {
            LastAttemptUtc = DateTime.UtcNow;
        }
    }
}
