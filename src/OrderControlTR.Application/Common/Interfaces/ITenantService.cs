namespace OrderControlTR.Application.Common.Interfaces;

public interface ITenantService
{
    int? GetCurrentRestaurantId();
    void SetCurrentRestaurantId(int restaurantId);
}
