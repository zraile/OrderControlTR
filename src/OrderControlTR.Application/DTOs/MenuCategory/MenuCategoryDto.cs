namespace OrderControlTR.Application.DTOs.MenuCategory;

public class MenuCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int RestaurantId { get; set; }
    public bool IsActive { get; set; }
}

public class CreateMenuCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public int RestaurantId { get; set; }
}

public class UpdateMenuCategoryDto
{
    public string Name { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
}
