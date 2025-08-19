using BookingClone.API.Extensions;
using BookingClone.Infrastructure.Extensions;
using BookingClone.Infrastructure.Jobs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApiServices()
    .AddInfrastructure(builder.Configuration)
    .ConfigureJwt()
    .AddJwtAuthentication(builder.Configuration)
    .AddApplicationServices();


var app = builder.Build();

//// Configure the HTTP request pipeline.
app.UseApiRequestPipeline();

HangfireJobs.ConfigureRecurringJobs();

app.Run();
