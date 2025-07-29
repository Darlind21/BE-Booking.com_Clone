namespace BookingClone.API.Helpers
{
    //We create custom Exception Class to return a standardized obj to the client in case an exception occurs to avoid leaking internal info 
    public class ApiException(int statusCode, string message, string? details)
    {
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public string? Details { get; set; } = details;
    }
}
