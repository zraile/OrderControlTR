namespace OrderControlTR.Domain.Common;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime InsertDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdateDate { get; set; }
    public int? InsertUserId { get; set; }
    public int? UpdateUserId { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;
}
