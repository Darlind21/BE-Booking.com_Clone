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
                // is the address of kafka cluster(the initial entry point).
                // A kafka cluster is a group of servers(called brokers) that work together to provide a distributed, fault-tolerat and scalable messaging system.
                //Once connected kafka automatically shares the cluster metadata e.g. how many brokers exist, what topics exist, how partitions are distributed etc.

                /*What makes up a kafka cluster? 
                 * 1.Brokers(servers running kafka)
                 * -each broker is identified by an Id
                 * -stores data for partitions and handles producer/consumer requests
                 * -typically a cluster has 3+ brokers for redundancy
                 * 2.Topics and Partitions
                 * -topic = logical channel e.g error-logs
                 * -a topic is divided into partitions.( Every topic is split into one or more partitions. Each partition is an ordered, immutable sequence of messages that is continually appended to) 
                 * 3.Replication
                 * -each partition is replicated to mtp brokers 
                 * -one replica is the leader, others are followers,
                 * -producers/consumers talk to leaders; followers are backup
                 */

                ClientId = "BookingClone.Api", //idetifies the producer
                Acks = Acks.All //means the produce call waits for all in-sync replicas to ackowledge that they got the message
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
