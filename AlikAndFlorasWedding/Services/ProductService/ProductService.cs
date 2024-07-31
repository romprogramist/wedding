using AlikAndFlorasWedding.Data;
using AlikAndFlorasWedding.Helpers;
using AlikAndFlorasWedding.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using AlikAndFlorasWedding.Models;

namespace AlikAndFlorasWedding.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _environment;

    public ProductService(DataContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    
    public async Task<IEnumerable<ProductDto>> GetProductsAsync(int? categoryId = null)
    {
        return await _context.Products
            .Where(p => categoryId == null || p.CategoryId == categoryId)
            .Include(p => p.Category)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Model = p.Model,
                ShortDescription = p.ShortDescription,
                LongDescription = p.LongDescription,
                Image = p.Image,
                ImageThumbnail = p.ImageThumbnail,
                Manufacturer = p.Manufacturer,
                CategoryId = p.CategoryId,
                CategoryDto = new CategoryDto
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                    Description = p.Category.Description,
                    Image = p.Category.Image,
                    ImageThumbnail = p.Category.ImageThumbnail,
                    Order = p.Category.Order,
                    IsAvailable = p.Category.IsAvailable
                },
                Price = p.Price,
                Color = p.Color,
                Weight = p.Weight,
                NumberInStock = p.NumberInStock,
                Width = p.Width,
                Height = p.Height,
                Length = p.Length,
                ExpirationDate = p.ExpirationDate,
                CreatedDate = p.CreatedDate,
                UpdatedDate = p.UpdatedDate,
                IsAvailable = p.IsAvailable,
                IsBestSeller = p.IsBestSeller,
                IsOnSale = p.IsOnSale
            }).ToListAsync();
    }
    
    public async Task<ProductDto?> GetProductAsync(int id)
    {
        return await _context.Products
            .Where(p => p.Id == id)
            .Include(p => p.Category)
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Model = p.Model,
                // ShortDescription = p.ShortDescription,
                // LongDescription = p.LongDescription,
                Image = p.Image,
                // ImageThumbnail = p.ImageThumbnail,
                // Manufacturer = p.Manufacturer,
                // CategoryId = p.CategoryId,
                // CategoryDto = new CategoryDto
                // {
                //     Id = p.Category.Id,
                //     Name = p.Category.Name,
                //     Description = p.Category.Description,
                //     Image = p.Category.Image,
                //     ImageThumbnail = p.Category.ImageThumbnail,
                //     Order = p.Category.Order,
                //     IsAvailable = p.Category.IsAvailable
                // },
                // Price = p.Price,
                // Color = p.Color,
                // Weight = p.Weight,
                // NumberInStock = p.NumberInStock,
                // Width = p.Width,
                // Height = p.Height,
                // Length = p.Length,
                // ExpirationDate = p.ExpirationDate,
                // CreatedDate = p.CreatedDate,
                // UpdatedDate = p.UpdatedDate,
                // IsAvailable = p.IsAvailable,
                // IsBestSeller = p.IsBestSeller,
                // IsOnSale = p.IsOnSale
            }).FirstOrDefaultAsync();
    }

    public async Task<bool> AddProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Model = productDto.Model,
            // ShortDescription = productDto.ShortDescription,
            // LongDescription = productDto.LongDescription,
            Image = productDto.Image,
            // ImageThumbnail = productDto.ImageThumbnail,
            // Manufacturer = productDto.Manufacturer,
            CategoryId = productDto.CategoryId,
            // Price = productDto.Price,
            // Color = productDto.Color,
            // Weight = productDto.Weight,
            // NumberInStock = productDto.NumberInStock,
            // Width = productDto.Width,
            // Height = productDto.Height,
            // Length = productDto.Length,
            // ExpirationDate = productDto.ExpirationDate,
            // CreatedDate = DateTime.Now,
            // UpdatedDate = DateTime.Now,
            // IsAvailable = productDto.IsAvailable,
            // IsBestSeller = productDto.IsBestSeller,
            // IsOnSale = productDto.IsOnSale
        };
        
        _context.Products.Add(product);
    
        var savedCount = await _context.SaveChangesAsync();
    
        return savedCount > 0;
    }
    
    
    public async Task<string> SaveProductImageAsync(IFormFile file)
    {
        var uniqueFileName = FileHelper.GetUniqueFileName(file.FileName);
        var uploadDirectory = Path.Combine(_environment.WebRootPath, "images", "products");
        var filePath = Path.Combine(uploadDirectory, uniqueFileName);
        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? throw new InvalidOperationException());
        await file.CopyToAsync(new FileStream(filePath, FileMode.Create));
        return uniqueFileName;
    }
    
    public async Task<bool> DeleteProductAsync(int id)
    {
        var company = await _context.Products.FindAsync(id);
        if (company is null)
            return false;
    
        _context.Products.Remove(company);
        var savedCount = await _context.SaveChangesAsync();
    
        return savedCount > 0;
    }

    public async Task<ProductDto?> UpdateProductAsync(ProductDto product)
    {
        var productToUpdate = await _context.Products.FindAsync(product.Id);
        if (productToUpdate is null)
            return null;
        
        productToUpdate.Name = product.Name;
        productToUpdate.Model = product.Model;
        // productToUpdate.ShortDescription = product.ShortDescription;
        // productToUpdate.LongDescription = product.LongDescription;
        
        if (!string.IsNullOrEmpty(product.Image))
        {
            productToUpdate.Image = product.Image;
        }
        
        // productToUpdate.ImageThumbnail = product.ImageThumbnail;
        // productToUpdate.Manufacturer = product.Manufacturer;
        // productToUpdate.CategoryId = product.CategoryId;
        // productToUpdate.Price = product.Price;
        // productToUpdate.Color = product.Color;
        // productToUpdate.Weight = product.Weight;
        // productToUpdate.NumberInStock = product.NumberInStock;
        // productToUpdate.Width = product.Width;
        // productToUpdate.Height = product.Height;
        // productToUpdate.Length = product.Length;
        // productToUpdate.ExpirationDate = product.ExpirationDate;
        // productToUpdate.UpdatedDate = DateTime.Now;
        // productToUpdate.IsAvailable = product.IsAvailable;
        // productToUpdate.IsBestSeller = product.IsBestSeller;
        // productToUpdate.IsOnSale = product.IsOnSale;
        
        _context.Products.Update(productToUpdate);
        
        var savedCount = await _context.SaveChangesAsync();
        if (savedCount <= 0)
            return null;

        var productDto = new ProductDto
        {
            Id = productToUpdate.Id,
            Name = productToUpdate.Name,
            Model = productToUpdate.Model,
            // ShortDescription = productToUpdate.ShortDescription,
            // LongDescription = productToUpdate.LongDescription,
            Image = productToUpdate.Image,
            // ImageThumbnail = productToUpdate.ImageThumbnail,
            // Manufacturer = productToUpdate.Manufacturer,
            // CategoryId = productToUpdate.CategoryId,
            // Price = productToUpdate.Price,
            // Color = productToUpdate.Color,
            // Weight = productToUpdate.Weight,
            // NumberInStock = productToUpdate.NumberInStock,
            // Width = productToUpdate.Width,
            // Height = productToUpdate.Height,
            // Length = productToUpdate.Length,
            // ExpirationDate = productToUpdate.ExpirationDate,
            // CreatedDate = productToUpdate.CreatedDate,
            // UpdatedDate = productToUpdate.UpdatedDate,
            // IsAvailable = productToUpdate.IsAvailable,
            // IsBestSeller = productToUpdate.IsBestSeller,
            // IsOnSale = productToUpdate.IsOnSale
        };

        return productDto;
    }
}