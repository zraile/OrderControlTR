using OrderControlTR.Domain.Common;

namespace OrderControlTR.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? TaxNumber { get; set; }
    public string? LogoUrl { get; set; }

    public ICollection<Branch> Branches { get; set; } = new List<Branch>();
    public ICollection<MenuCategory> MenuCategories { get; set; } = new List<MenuCategory>();
    public ICollection<User> Users { get; set; } = new List<User>();
}
