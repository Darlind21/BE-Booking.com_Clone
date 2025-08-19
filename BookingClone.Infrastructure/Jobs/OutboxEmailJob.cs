using BookingClone.Application.Common.Interfaces;
using BookingClone.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Jobs
{
    public class OutboxEmailJob (IOutboxProcessor processor)
    {
        public async Task ProcessSingleEmail(Guid messageId)
        {
            await processor.ProcessSingleMessage(messageId);
        }

        public async Task ProcessPendingEmails()
        {
            await processor.ProcessPendingMessages();
        }
    }
}
