using OrderControlTR.Application.Common.Interfaces;

namespace OrderControlTR.Infrastructure.Services;

// Registered as Scoped (per-request) in DependencyInjection.cs.
// The instance field is safe because each HTTP request gets its own instance.
public class TenantService : ITenantService
{
    private int? _currentRestaurantId;

    public int? GetCurrentRestaurantId() => _currentRestaurantId;

    public void SetCurrentRestaurantId(int restaurantId)
        => _currentRestaurantId = restaurantId;
}
