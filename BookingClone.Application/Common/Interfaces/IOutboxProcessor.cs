using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Common.Interfaces
{
    public interface IOutboxProcessor
    {
        Task ProcessPendingMessages(int batchSize = 50);
        Task ProcessSingleMessage(Guid messageId);
    }
}
