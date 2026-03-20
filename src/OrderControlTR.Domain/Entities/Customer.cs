using OrderControlTR.Domain.Common;

namespace OrderControlTR.Domain.Entities;

public class Customer : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Notes { get; set; }
    public int? RestaurantId { get; set; }

    public Restaurant? Restaurant { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
