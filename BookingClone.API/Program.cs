using BookingClone.API.Extensions;
using BookingClone.API.Middleware;
using BookingClone.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApiServices()
    .AddInfrastructure(builder.Configuration)
    .ConfigureJwt()
    .AddApplicationServices();


var app = builder.Build();

//// Configure the HTTP request pipeline.
app.UseApiRequestPipeline();

app.Run();
