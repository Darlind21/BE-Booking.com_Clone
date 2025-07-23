using BookingClone.Application.Common.Behaviours;
using FluentValidation;
using MediatR;

namespace BookingClone.API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
            var assembly = typeof(BookingClone.Application.Features.User.Commands.RegisterUser.RegisterUserCommand).Assembly;

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            return services;
        }
    }
}
