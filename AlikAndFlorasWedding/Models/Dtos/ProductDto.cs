namespace AlikAndFlorasWedding.Models.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Model { get; set; } = null!;
    public string ShortDescription { get; set; } = null!;
    public string LongDescription { get; set; } = null!;
    public string Image { get; set; } = null!;
    public string ImageThumbnail { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public int CategoryId { get; set; }
    public CategoryDto CategoryDto { get; set; } = null!;
    public decimal Price { get; set; }
    public string Color { get; set; } = null!;
    public int Weight { get; set; }
    public int NumberInStock { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int Length { get; set; }
    public DateTime ExpirationDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsBestSeller { get; set; }
    public bool IsOnSale { get; set; }
}