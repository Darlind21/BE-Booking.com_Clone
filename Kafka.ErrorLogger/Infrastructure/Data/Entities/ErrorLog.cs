using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Kafka.ErrorLogger.Infrastructure.Data.Entities
{
    public class ErrorLog
    {
        public Guid Id { get; set; }
        public DateTime OccurredAtUtc { get; set; }
        public required string ServiceName { get; set; }
        public required string Environment { get; set; }
        public required string Severity { get; set; }
        public required string ExceptionType { get; set; }
        public required string Message { get; set; }
        public required string StackTrace { get; set; }
        public required string HttpMethod { get; set; }
        public required string Path { get; set; }
        public required string QueryString { get; set; }
        public required Guid? UserId { get; set; }
        public required string TraceId { get; set; }
        public required string RawJson { get; set; }
        public DateTime InsertedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
