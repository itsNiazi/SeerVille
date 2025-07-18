using System.Net;

namespace Backend.Middleware;

// Simple IP filtering middleware.
// Would change to blocklist & let proxy server handle it.
// How to handle time complexity O(n), with big list?
public class IpFilterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly byte[][] _safelist; // would change to a block list (external use).

    public IpFilterMiddleware(RequestDelegate next, string safelist)
    {
        var ips = safelist.Split(";"); // currently only allows own IP (localhost);
        _safelist = new byte[ips.Length][];
        for (var i = 0; i < ips.Length; i++)
        {
            _safelist[i] = IPAddress.Parse(ips[i]).GetAddressBytes();
        }

        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var remoteIp = context.Connection.RemoteIpAddress;

        var bytes = remoteIp.GetAddressBytes();
        var badIp = true;
        foreach (var address in _safelist)
        {
            if (address.SequenceEqual(bytes))
            {
                badIp = false;
                break;
            }
        }

        if (badIp)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return;
        }

        await _next.Invoke(context);
    }
}
