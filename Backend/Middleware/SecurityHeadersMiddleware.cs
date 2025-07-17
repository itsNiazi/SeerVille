
public class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;

    public SecurityHeadersMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Content-Security-Policy
        if (!context.Response.Headers.ContainsKey("Content-Security-Policy"))
        {
            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';base-uri 'self';font-src 'self' https: data:;form-action 'self';frame-ancestors 'self';img-src 'self' data:;object-src 'none';script-src 'self';script-src-attr 'none';style-src 'self' https: 'unsafe-inline';upgrade-insecure-requests");
        }

        // Cross-Origin-Opener-Policy
        if (!context.Response.Headers.ContainsKey("Cross-Origin-Opener-Policy"))
        {
            context.Response.Headers.Add("Cross-Origin-Opener-Policy", "same-origin");
        }

        // Cross-Origin-Resource-Policy
        if (!context.Response.Headers.ContainsKey("Cross-Origin-Resource-Policy"))
        {
            context.Response.Headers.Add("Cross-Origin-Resource-Policy", "same-origin");
        }

        // Origin-Agent-Cluster
        if (!context.Response.Headers.ContainsKey("Origin-Agent-Cluster"))
        {
            context.Response.Headers.Add("Origin-Agent-Cluster", "?1");
        }

        // Referrer-Policy
        if (!context.Response.Headers.ContainsKey("Referrer-Policy"))
        {
            context.Response.Headers.Add("Referrer-Policy", "no-referrer");
        }

        // Strict-Transport-Security
        if (!context.Response.Headers.ContainsKey("Strict-Transport-Security"))
        {
            context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
        }

        // X-Content-Type-Options
        if (!context.Response.Headers.ContainsKey("X-Content-Type-Options"))
        {
            context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
        }

        // X-Frame-Options
        if (!context.Response.Headers.ContainsKey("X-Frame-Options"))
        {
            context.Response.Headers.Add("X-Frame-Options", "DENY");
        }

        // X-Permitted-Cross-Domain-Policies
        if (!context.Response.Headers.ContainsKey("X-Permitted-Cross-Domain-Policies"))
        {
            context.Response.Headers.Add("X-Permitted-Cross-Domain-Policies", "none");
        }

        // X-XSS-Protection
        if (!context.Response.Headers.ContainsKey("X-XSS-Protection"))
        {
            context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
        }

        await _next(context);
    }
}