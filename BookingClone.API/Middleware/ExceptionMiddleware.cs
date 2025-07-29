using BookingClone.API.Helpers;
using System.Net;
using System.Text.Json;

namespace BookingClone.API.Middleware
{
    //We create custom Exception middleware because since exceptions may occur (or we throw them) in the application/domain layer,
    //we can catch those and return a standardized ApiException(custom class) dto to the client to avoid leaking internal info (like stack traces)
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    //RequestDelegate is a built-in delegate (a variable that points to a method) that represents a function that can process an http request and returns a Task
        //it is how middleware components call each other in the pipeline
        //we use it so when writing custom middleware to call the next middleware in the pipeline.

    //ILogger<T> is used to log errors for : - debugging , - tracking app behaviour, -diagnosing failures -- it is automatically registered in the DI container

    //IHostEnvironment is a built in .net interface that provides info about the current hosting environment the app is running in - it is registered automatically in the DI container
    {
        public async Task InvokeAsync(HttpContext context)
        //In order for .net runtime to recognise the class as middleware the method has to be named EXACTLY Invoke or InvokeAsync. The method should also be public, return a Task and take a single parameter of type HttpContext.
            //The framework automatically calls this method during each HTTP request and passes the HttpContext instance.
            //Internally .net uses reflection to locate a method named Invoke or InvokeAsync with the correct signature to run it as middleware
            //Reflection is a feature in .net that lets the code look at itself while its running - it can can inspect and even interact with its own classes, methods, props etc

        //HttpContext is a built-int class that represents everything about a single HTTP request and response - the full context of what the client send and what the server will return.
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json"; //sets the response content-type header to json - ensures client receives a proper json error response
                //if we do not expilictly set it to json the response will have no content-type header - we would be sending a raw string to the client,
                //which it has no reliable way to interpret without knowing what the string represents

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace)
                    : new ApiException(context.Response.StatusCode, ex.Message, "An unexpected error has occured");

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options); //converts the ApiException obj into a JSON string

                await context.Response.WriteAsync(json); //writes the json error to the response body so the client can see it 
            }
        }
    }
}
