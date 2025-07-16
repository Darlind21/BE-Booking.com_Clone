using BookingClone.Infrastructure.Data;
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
    public static class DbContextExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<BookingDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));


            return services;
        }
    }
}
