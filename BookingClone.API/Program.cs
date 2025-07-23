using BookingClone.API.Extensions;
using BookingClone.API.Middleware;
using BookingClone.Infrastructure.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApiServices();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.ConfigureJwt(builder.Configuration);

builder.Services.AddApplicationServices();


var app = builder.Build();

//// Configure the HTTP request pipeline.
app.UseApiRequestPipeline();

app.Run();
