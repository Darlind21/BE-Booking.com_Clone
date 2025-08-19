using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Domain.Entities;
using BookingClone.Infrastructure.Data;
using BookingClone.Infrastructure.Services.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Services
{
    public class OutboxProcessor(BookingDbContext db, IEmailService emailService, ILogger<OutboxProcessor> logger) : IOutboxProcessor
    {
        public async Task ProcessSingleMessage(Guid messageId)
        {
            var message = await db.OutboxMessages.FindAsync(messageId);
            if (message == null || message.ProcessedOnUtc != null || message.RetryCount >= message.MaxRetries)
                return;
            await ProcessMessage(message);
        }


        public async Task ProcessPendingMessages(int batchSize = 50)
        {
            var pendingMessages = await db.OutboxMessages
                .Where(m => m.ProcessedOnUtc == null && m.RetryCount < m.MaxRetries)
                .OrderBy(m => m.OccurredOnUtc)
                .Take(batchSize)
                .ToListAsync();

            foreach (var message in pendingMessages)
            {
                if (message == null || message.ProcessedOnUtc != null || message.RetryCount >= message.MaxRetries)
                    return;
                await ProcessMessage(message);
            }
        }

        private async Task ProcessMessage(OutboxMessage message)
        {
            try
            {
                var payload = JsonSerializer.Deserialize<EmailPayload>(message.Payload);
                if (payload != null)
                {
                    await emailService.SendEmailAsync(payload.To, payload.Subject, payload.Body);
                }

                message.MarkProcessed();
            }
            catch (Exception ex)
            {
                message.SetError(ex.Message); //also updates lastattempt and retry count
                logger.LogError(ex, "Failed to process OutboxMessage {MessageId}", message.Id);
            }

            await db.SaveChangesAsync();
        }

        private record EmailPayload(string To, string Subject, string Body);
    }
}
