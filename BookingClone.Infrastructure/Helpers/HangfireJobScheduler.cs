using BookingClone.Application.Common.Interfaces;
using BookingClone.Infrastructure.Services;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Helpers
{
    public class HangfireJobScheduler : IJobScheduler
    {
        //public void Enqueue<TJob>(Expression<Func<TJob, Task>> methodCall) where TJob : class
        //{
        //    BackgroundJob.Enqueue(methodCall);
        //}

        public void EnqueueOutboxMessage(Guid messageId)
        {
            // Directly enqueue the job using Hangfire's static method
            BackgroundJob.Enqueue<OutboxProcessor>(x => x.ProcessSingleMessage(messageId));
        }
    }
}
