using OrderControlTR.Domain.Common;
using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Domain.Entities;

public class Order : BaseEntity
{
    public int? TableId { get; set; }
    public int BranchId { get; set; }
    public int UserId { get; set; }
    public OrderType OrderType { get; set; } = OrderType.DineIn;
    public OrderStatus Status { get; set; } = OrderStatus.New;
    public decimal TotalAmount { get; set; }
    public string? Notes { get; set; }
    public int? CustomerId { get; set; }

    public Table? Table { get; set; }
    public Branch Branch { get; set; } = null!;
    public User User { get; set; } = null!;
    public Customer? Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
