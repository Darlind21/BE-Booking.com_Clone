using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Jobs
{
    public static class HangfireJobs
    {
        public static void ConfigureRecurringJobs()
        {
            RecurringJob.AddOrUpdate<OutboxEmailJob>(
                job => job.ProcessPendingEmails(),
                Cron.Minutely
            );
        }
    }
}
