using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Infrastructure.Data;
using BookingClone.Infrastructure.Helpers;
using BookingClone.Infrastructure.Repositories;
using BookingClone.Infrastructure.Services;
using BookingClone.Infrastructure.Services.Cloudinary;
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

            services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            services.AddScoped<IApartmentPhotoRepository, ApartmentPhotoRepository>();

            services.AddScoped<IPaginationHelper, PaginationHelper>();




            return services;
        }

        public static IServiceCollection ConfigureJwt(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            return services;
        }
    }
}
