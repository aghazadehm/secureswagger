using SecureSwagger.Middlewares;

namespace SecureSwagger.Extensions
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}