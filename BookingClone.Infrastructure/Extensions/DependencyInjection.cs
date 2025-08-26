using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Infrastructure.Data;
using BookingClone.Infrastructure.Helpers;
using BookingClone.Infrastructure.Jobs;
using BookingClone.Infrastructure.Repositories;
using BookingClone.Infrastructure.Services;
using BookingClone.Infrastructure.Services.Cloudinary;
using BookingClone.Infrastructure.Services.Email;
using BookingClone.Infrastructure.SignalR.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BookingDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IApartmentRepository, ApartmentRepository>();
            services.AddScoped<IApartmentPhotoRepository, ApartmentPhotoRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<IOutboxRepository, OutboxRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddSignalR();
            services.AddScoped<INotificationService, SignalRNotificationService>();

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            services.AddScoped<IPaginationHelper, PaginationHelper>();


            var emailSettings = config
                .GetSection("EmailSettings")
                .Get<EmailSettings>() ?? throw new Exception("Unable to get EmailSettings from appsettings.json");

            //so it's injectable anywhere
            services.AddSingleton(emailSettings);

            services.AddScoped<IEmailService> (provider =>
                new EmailService(emailSettings.SmtpServer, emailSettings.Port, emailSettings.FromEmail, emailSettings.FromPassword));

            services.AddScoped<IOutboxProcessor, OutboxProcessor>();
            services.AddScoped<OutboxEmailJob>();
            services.AddScoped<IJobScheduler, HangfireJobScheduler>();





            string connectionString = config.GetConnectionString("DefaultConnection")!;
            services.AddHangfire(config =>
                config.UseSqlServerStorage(connectionString));
            services.AddHangfireServer();


            return services;
        }

        public static IServiceCollection ConfigureJwt(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
