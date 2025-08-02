using System.Security.Claims;

namespace BookingClone.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        //ClaimsPrincipal is a .net class that represents the currently authenticated user
        //It holds the user's identity and a list of their claims 
        //In short it gives you access to all the claims for the current user
        //.Net automatically builds a ClaimsPrincipal obj from the JWT token or cookie when a request is made to a secure endpoint that has [Authorize]
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Guid.TryParse(userIdClaim, out var id) 
                ? id 
                : throw new Exception("Invalid user Id in token");
        }
    }
}
