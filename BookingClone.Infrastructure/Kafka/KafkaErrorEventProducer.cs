using BookingClone.Application.Interfaces.Kafka;
using BookingClone.Shared.Messaging.Events.BookingClone.Shared.Messaging;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Kafka
{
    public class KafkaErrorEventProducer : IErrorEventProducer
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public KafkaErrorEventProducer(string bootstrapServers, string topic)
        {
            _topic = topic;

            var config = new ProducerConfig
            {
                BootstrapServers = bootstrapServers,
                ClientId = "BookingClone.Api",
                Acks = Acks.All
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task ProduceAsync(ErrorEvent errorEvent)
        {
            var json = JsonSerializer.Serialize(errorEvent);
            await _producer.ProduceAsync(_topic, new Message<string, string>
            {
                Key = errorEvent.EventId.ToString(),
                Value = json
            });
        }
    }
}
