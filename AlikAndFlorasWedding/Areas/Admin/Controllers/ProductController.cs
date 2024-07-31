using AlikAndFlorasWedding.Models;
using AlikAndFlorasWedding.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace AlikAndFlorasWedding.Areas.Admin.Controllers;

public class ProductController : BaseAdminController
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductAsync(id);
        
        if(product == null)
            return NotFound("Product not found.");
        
        var productModel = new ProductModel
        {
            Id = product.Id,
            Name = product.Name,
            Model = product.Model,
            ImageUrl = product.Image
        };
        return View(productModel);
    }
}