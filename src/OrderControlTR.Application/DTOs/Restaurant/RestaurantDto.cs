namespace OrderControlTR.Application.DTOs.Restaurant;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? TaxNumber { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; }
}

public class CreateRestaurantDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? TaxNumber { get; set; }
    public string? LogoUrl { get; set; }
}

public class UpdateRestaurantDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? TaxNumber { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; }
}
