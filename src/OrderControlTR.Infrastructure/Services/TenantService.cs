using OrderControlTR.Application.Common.Interfaces;

namespace OrderControlTR.Infrastructure.Services;

public class TenantService : ITenantService
{
    private int? _currentRestaurantId;

    public int? GetCurrentRestaurantId() => _currentRestaurantId;

    public void SetCurrentRestaurantId(int restaurantId)
        => _currentRestaurantId = restaurantId;
}
