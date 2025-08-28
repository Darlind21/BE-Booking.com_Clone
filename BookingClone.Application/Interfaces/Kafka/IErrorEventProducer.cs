using BookingClone.Shared.Messaging.Events.BookingClone.Shared.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Kafka
{
    public interface IErrorEventProducer
    {
        Task ProduceAsync(ErrorEvent errorEvent);
    }
}
