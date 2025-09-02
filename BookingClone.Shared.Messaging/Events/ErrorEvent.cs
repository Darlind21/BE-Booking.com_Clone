using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Shared.Messaging.Events
    /*This class library is the contract between the producer and consumer. It is a schema contract that
     * reduces bugs and enables safe refactoring and versioning. So instead of having two seperate records in each project we create a
     * shared project that defines data that will be shared from producer to consumer. It is a single source of truth 
     * 
     * 1. It ensures both sides agree on the shape of the message.
     * 2. It gives you compile-time typing in both services
     * 3. It prevents mismatches
    */
{
    namespace BookingClone.Shared.Messaging
    {
        public record ErrorEvent
        {
            public Guid EventId { get; init; } = Guid.NewGuid();
            public DateTime OccurredAtUtc { get; init; } = DateTime.UtcNow;
            public string ServiceName { get; init; } = null!;
            public string Environment { get; init; } = null!;
            public string Severity { get; init; } = null!; //(e.g., Error, Warning, Critical).


            public string? HttpMethod { get; init; } //Useful to see if errors are tied to certain request types.
            public string? Path { get; init; } //Helps trace which part of the API is failing.
            public string? QueryString { get; init; } //Helps debug edge cases (e.g., maybe only failing for certain query values).
            public Guid? UserId { get; init; } //Useful for customer support


            public string? ExceptionType { get; init; }
            public string? Message { get; init; }
            public string? StackTrace { get; init; }
            public string? InnerException { get; init; }


            public string? TraceId { get; init; }


            public string? RawJson { get; init; }
        }
    }

}
