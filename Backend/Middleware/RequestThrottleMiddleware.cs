using System.Security.Claims;
using Microsoft.Extensions.Caching.Memory;

public class RequestThrottleMiddleware
{
    private readonly RequestDelegate _next;
    // Look into memory issues here
    private static readonly MemoryCache _requestCache = new(new MemoryCacheOptions());

    private readonly int _maxRequests;
    private readonly TimeSpan _timeWindow;
    private readonly TimeSpan _fixedDelay;

    public RequestThrottleMiddleware(RequestDelegate next, int maxRequests, TimeSpan timeWindow, TimeSpan fixedDelay)
    {
        _next = next;
        _maxRequests = maxRequests;
        _timeWindow = timeWindow;
        _fixedDelay = fixedDelay;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                     ?? context.Connection.RemoteIpAddress?.ToString()
                     ?? "anonymous";

        var cacheKey = $"Throttle_{userId}";

        var entry = _requestCache.GetOrCreate(cacheKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = _timeWindow;
            return new RequestCounter();
        });

        if (entry.Count >= _maxRequests)
        {
            // Introduce delay before processing the request
            await Task.Delay(_fixedDelay);
        }
        else
        {
            entry.Count++;
        }

        await _next(context);
    }

    private class RequestCounter
    {
        public int Count { get; set; } = 0;
    }
}
