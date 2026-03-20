using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Application.DTOs.OrderItem;

public class OrderItemDto
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public string? Notes { get; set; }
    public OrderItemStatus Status { get; set; }
}

public class CreateOrderItemDto
{
    public int OrderId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public string? Notes { get; set; }
}

public class UpdateOrderItemDto
{
    public int Quantity { get; set; }
    public string? Notes { get; set; }
    public OrderItemStatus Status { get; set; }
}
