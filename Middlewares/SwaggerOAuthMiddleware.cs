using Microsoft.AspNetCore.Authentication;

namespace SecureSwagger.Middlewares
{
  /// <summary>
  /// https://medium.com/@niteshsinghal85/securing-swagger-ui-in-production-in-asp-net-core-part-2-dc2ae0f03c73
  /// </summary>
  public class SwaggerOAuthMiddleware
  {
    private readonly RequestDelegate next;
    public SwaggerOAuthMiddleware(RequestDelegate next)
    {
      this.next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
      if (IsSwaggerUI(context.Request.Path))
      {
        // if user is not authenticated
        if (!context.User.Identity.IsAuthenticated)
        {
          await context.ChallengeAsync();
          return;
        }
      }
      await next.Invoke(context);
    }
    public bool IsSwaggerUI(PathString pathString)
    {
      return pathString.StartsWithSegments("/swagger");
    }
  }
}