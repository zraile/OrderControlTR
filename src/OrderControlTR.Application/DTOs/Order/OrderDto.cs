using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Application.DTOs.Order;

public class OrderDto
{
    public int Id { get; set; }
    public int? TableId { get; set; }
    public int BranchId { get; set; }
    public int UserId { get; set; }
    public OrderType OrderType { get; set; }
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public int? CustomerId { get; set; }
    public DateTime InsertDate { get; set; }
}

public class CreateOrderDto
{
    public int? TableId { get; set; }
    public int BranchId { get; set; }
    public int UserId { get; set; }
    public OrderType OrderType { get; set; } = OrderType.DineIn;
    public string? Notes { get; set; }
    public int? CustomerId { get; set; }
}

public class UpdateOrderDto
{
    public OrderType OrderType { get; set; }
    public string? Notes { get; set; }
    public int? CustomerId { get; set; }
}

public class UpdateOrderStatusDto
{
    public OrderStatus Status { get; set; }
}
