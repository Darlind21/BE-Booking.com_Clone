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
        private readonly IConfiguration _config = config;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _config["Kafka:BootstrapServers"],
                GroupId = "errorlogger-svc",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            consumer.Subscribe(_config["Kafka:TopicName"]);

            while (!stoppingToken.IsCancellationRequested)
            {
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
