using OrderControlTR.Domain.Common;
using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Domain.Entities;

public class Table : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int BranchId { get; set; }
    public TableStatus Status { get; set; } = TableStatus.Available;

    public Branch Branch { get; set; } = null!;
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
