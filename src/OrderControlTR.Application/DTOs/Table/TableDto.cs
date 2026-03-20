using OrderControlTR.Domain.Enums;

namespace OrderControlTR.Application.DTOs.Table;

public class TableDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int BranchId { get; set; }
    public TableStatus Status { get; set; }
    public bool IsActive { get; set; }
}

public class CreateTableDto
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int BranchId { get; set; }
}

public class UpdateTableDto
{
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public TableStatus Status { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateTableStatusDto
{
    public TableStatus Status { get; set; }
}
