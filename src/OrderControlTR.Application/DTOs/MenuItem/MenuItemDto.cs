namespace OrderControlTR.Application.DTOs.MenuItem;

public class MenuItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsActive { get; set; }
}

public class CreateMenuItemDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public int CategoryId { get; set; }
    public string? ImageUrl { get; set; }
}

public class UpdateMenuItemDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsActive { get; set; }
}
