using BookingClone.Kafka.ErrorLogger.Infrastructure.Data;
using BookingClone.Kafka.ErrorLogger.Infrastructure.Data.Entities;
using BookingClone.Shared.Messaging.Events.BookingClone.Shared.Messaging;
using Confluent.Kafka;
using System.Text.Json;

namespace BookingClone.Kafka.ErrorLogger.Worker
{
    public class Worker(IServiceProvider services, IConfiguration config, ILogger<Worker> logger) : BackgroundService
    {
        private readonly IServiceProvider _services = services;
        /*IServiceProvider is the runtime DI Container. It is an obj provided by Microsoft DI system that knows how to build and hand out services you registered
         * Why is it needed in a background worker 
         * Worker class typically runs as singleton(hosted service). But DbContext is normally registered as scoped(one instance per logical operation(http request))
         * We must not inject a scoped service directly into a singleton because that scoped instance would become effectively singleton
         * Solution: Inject IServiceProvider and create a scope for each message. Each scope gives you its own IServiceProvider that will provide scoped instances that 
         * are disposed when the scope is disposed
         */
        private readonly IConfiguration _config = config;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _config["Kafka:BootstrapServers"],
                GroupId = "errorlogger-svc", 
                // Kafka groups consumers together using a groupId. If mtp worker instances run with the same groupId, Kafka will split up the topics partitions between
                //them so that each message is handled by only one worker in that group 

                AutoOffsetReset = AutoOffsetReset.Earliest,
                //When a new consumer group connects and kafka doesnt yet know where they left off this setting decides where t start reading messages
                //Earliest => go back to the begging of the topic and read all avb messages

                EnableAutoCommit = false
                //nornmally kafka will automatically keep track of "where you are"(the offset of the last message consumed) and update it for you 
                //by setting this to false, we are telling kafka "dont mark a message ass done until I say so"

                //In our worker we only call Commit() after we have successfully saved to the database and no errrors hjave occurred
            };

            using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            consumer.Subscribe(_config["Kafka:TopicName"]);

            while (!stoppingToken.IsCancellationRequested)
            {
                //Consume -> Deserialize -> Save -> Commit
                try
                {
                    var result = consumer.Consume(stoppingToken);
                    var errorEvent = JsonSerializer.Deserialize<ErrorEvent>(result.Message.Value);

                    using var scope = _services.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<ErrorLogDbContext>();

                    db.ErrorLogs.Add(new ErrorLog
                    {
                        Id = errorEvent!.EventId,
                        OccurredAtUtc = errorEvent.OccurredAtUtc,
                        ServiceName = errorEvent.ServiceName,
                        Environment = errorEvent.Environment,
                        Severity = errorEvent.Severity,
                        ExceptionType = errorEvent.ExceptionType!,
                        Message = errorEvent.Message!,
                        StackTrace = errorEvent.StackTrace!,
                        HttpMethod = errorEvent.HttpMethod!,
                        Path = errorEvent.Path!,
                        QueryString = errorEvent.QueryString!,
                        UserId = errorEvent.UserId,
                        TraceId = errorEvent.TraceId!,
                        RawJson = errorEvent.RawJson!
                    });

                    await db.SaveChangesAsync(stoppingToken);
                    consumer.Commit(result);

                    logger.LogInformation($"\n Message consumed !!!! \n");
                }
                catch (Exception ex)
                {
                    logger.LogError($"Error in consumer. Unable to log error on BookingClone.Kafka.ErrorLogger: \n {ex.Message}");

                    await Task.Delay(5000, stoppingToken);
                }
            }
        }
    }
}
