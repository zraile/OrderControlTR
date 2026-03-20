using OrderControlTR.Domain.Common;

namespace OrderControlTR.Domain.Entities;

public class Branch : BaseEntity
{
    public int RestaurantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public Restaurant Restaurant { get; set; } = null!;
    public ICollection<Table> Tables { get; set; } = new List<Table>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
