using BookingClone.API.Middleware;
using Hangfire;

namespace BookingClone.API.Extensions
{
    public static class ConfigureMiddlewarePipelineExtensions
    {
        public static WebApplication UseApiRequestPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //custom global error handling middleware 
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.UseHangfireDashboard();

            return app;
        }
    }
}
