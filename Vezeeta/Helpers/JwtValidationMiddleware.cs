using System.IdentityModel.Tokens.Jwt;

namespace Vezeeta.Helpers
{
    public class JwtValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            if (!string.IsNullOrEmpty(token))
            {
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(token);
                var userId = decodedToken.Claims.FirstOrDefault(claim => claim.Type == "uid")?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    context.Items["UserId"] = userId; // Store the user ID in HttpContext.Items
                }
            }

            await _next(context);
        }
    }
}
// Explain the HttpContext in Details 
// And all the code