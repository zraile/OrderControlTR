using OrderControlTR.Domain.Common;
using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Domain.Entities;

public class OrderItem : BaseEntity
{
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string? Notes { get; set; }
    public OrderItemStatus Status { get; set; } = OrderItemStatus.Pending;

    public Order Order { get; set; } = null!;
    public MenuItem MenuItem { get; set; } = null!;
}
