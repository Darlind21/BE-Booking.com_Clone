using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Interfaces
{
    public interface IJobScheduler
    {
        //void Enqueue<TJob>(Expression<Func<TJob, Task>> methodCall) where TJob : class;
        void EnqueueOutboxMessage(Guid messageId);
    }
}
