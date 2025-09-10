using System.Security.Claims;
using System.Threading.RateLimiting;

public static class RateLimiterExtension
{
    public static IServiceCollection AddGlobalRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Fallback to user IP adress
                var identifier = string.IsNullOrEmpty(userId)
                    ? httpContext.Connection.RemoteIpAddress?.ToString() ?? "anonymous" // needs to fix
                    : userId;

                Console.WriteLine(identifier);

                return RateLimitPartition.GetTokenBucketLimiter(identifier, _ => new TokenBucketRateLimiterOptions
                {
                    TokenLimit = 50,
                    QueueLimit = 0,
                    ReplenishmentPeriod = TimeSpan.FromMinutes(1),
                    TokensPerPeriod = 10,
                    AutoReplenishment = true
                });
            });

            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
        });

        return services;
    }
}