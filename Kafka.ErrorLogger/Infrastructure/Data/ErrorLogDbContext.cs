using BookingClone.Kafka.ErrorLogger.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Kafka.ErrorLogger.Infrastructure.Data
{
    public class ErrorLogDbContext(DbContextOptions<ErrorLogDbContext> options) : DbContext(options)
    {
        public DbSet<ErrorLog> ErrorLogs { get; set; }
    }
}
