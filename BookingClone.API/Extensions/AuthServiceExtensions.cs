using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookingClone.API.Extensions
{
    public static class AuthServiceExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
                //this tells .net to enable authentication in the app and use the JWT Bearer scheme by default
                //that means when someone tries to access a [Authorize] endpoint, the app will expect a JWT token in the Authorization header (like Bearer <token>)
            .AddJwtBearer(options => //adds support for jwt bearer tokens as the auth method...inside {} we can config how the tokens should be validated
            {
                var tokenKey = config["JwtConfig:TokenKey"] ?? throw new Exception("Unable to get token key from appsettings.json");

                options.TokenValidationParameters = new TokenValidationParameters //we are setting the rules that tell the app how to verify the token is valid and trusted 
                {
                    ValidateIssuer = false, //if we had JwtConfig: ValidIssuer, this validator would check the iss claim inside the Jwt and compare it, if they do not match the token is rejected. 
                    ValidateAudience = false,
                    ValidateLifetime = true, //if current datetime is after exp claim inside the jwt the token is rejected
                    ValidateIssuerSigningKey = true, //checks if token has been signed with a valid security key
                    IssuerSigningKey = new SymmetricSecurityKey( // this gets the secret key used to verify the token signature and turns it into a SymmetricSecurityKey by converting the string from config into bytes
                        Encoding.UTF8.GetBytes(tokenKey)
                    )
                };
            });

            return services;
        }
    }
}
