using OrderControlTR.Application.Common.Interfaces;

namespace OrderControlTR.API.Middleware;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantService tenantService)
    {
        var restaurantIdHeader = context.Request.Headers["X-Restaurant-Id"].FirstOrDefault();

        if (!string.IsNullOrEmpty(restaurantIdHeader) && int.TryParse(restaurantIdHeader, out var restaurantId))
        {
            tenantService.SetCurrentRestaurantId(restaurantId);
        }

        await _next(context);
    }
}
