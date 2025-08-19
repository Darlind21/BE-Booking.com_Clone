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
    public class OutboxRepository(BookingDbContext context) : BaseRepository<OutboxMessage>(context), IOutboxRepository
    {
        private readonly BookingDbContext context = context;
        public async Task<List<OutboxMessage>> GetPendingMessagesAsync(int batchSize)
        {
            return await context.OutboxMessages
                .Where(m => m.ProcessedOnUtc == null && m.RetryCount < m.MaxRetries)
                .OrderBy(m => m.OccurredOnUtc)
                .Take(batchSize)
                .ToListAsync();
        }
    }
}
