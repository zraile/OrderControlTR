using OrderControlTR.Domain.Common;
using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Domain.Entities;

public class Payment : BaseEntity
{
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

    public Order Order { get; set; } = null!;
}
