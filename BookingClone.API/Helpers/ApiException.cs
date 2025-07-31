namespace BookingClone.API.Helpers
{
    //We create custom Exception Class to return a standardized obj to the client in case an exception occurs to avoid leaking internal info 
    public class ApiException(int statusCode, string message, List<string>? errors = null, string? details = null)
    {
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public List<string>? Errors { get; set; } = errors;
        public string? Details { get; set; } = details;
    }
}
