using BookingClone.Kafka.ErrorLogger.Extensions;
using BookingClone.Kafka.ErrorLogger.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services
    .AddHostedService<Worker>()
    .AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
