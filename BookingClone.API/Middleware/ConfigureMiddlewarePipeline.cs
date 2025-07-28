namespace BookingClone.API.Middleware
{
    public static class ConfigureMiddlewarePipeline
    {
        public static WebApplication UseApiRequestPipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.UseCustomExceptionMiddleware();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
