namespace AlikAndFlorasWedding.Data;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string ImageThumbnail { get; set; } = string.Empty;
    public string Manufacturer { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
    public decimal Price { get; set; }
    public string Color { get; set; } = string.Empty;
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