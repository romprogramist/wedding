using AlikAndFlorasWedding.Models;
using AlikAndFlorasWedding.Models.Dtos;
using AlikAndFlorasWedding.Services.ProductService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlikAndFlorasWedding.Controllers.API;

[ApiController]
[Route("api/product")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetProducts()
    {
        var products = await _productService.GetProductsAsync();
        return Ok(products.Select(p => new ProductModel
        {
            Id = p.Id,
            Name = p.Name,
            Model = p.Model,
            ImageUrl = p.Image
        }).OrderBy(p => p.Id));
    }
    
    [HttpPost]
    [Route("add")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddProduct([FromForm] ProductModel product)
    {
        var requestFiles = Request.Form.Files;
        if (requestFiles.Count > 0)
        {
            if (requestFiles[0].Length > 1024 * 1024)
            {
                return BadRequest("File size is too large.");
            }
            product.ImageUrl = await _productService.SaveProductImageAsync(requestFiles[0]);
        }
        else
        {
            product.ImageUrl = string.Empty;
        }
        
        var productDto = new ProductDto
        {
            Name = product.Name,
            Model = product.Model,
            Image = product.ImageUrl,
            CategoryId = product.CategoryId
        };
        var saved = await _productService.AddProductAsync(productDto);
        if(!saved)
            return BadRequest();
        return Ok();
    }
    
    [HttpPost]
    [Route("update")]
    // [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct([FromForm] ProductModel product)
    {
        var requestFiles = Request.Form.Files;
        if (requestFiles.Count > 0)
        {
            if (requestFiles[0].Length > 1024 * 1024)
            {
                return BadRequest("File size is too large.");
            }
            product.ImageUrl = await _productService.SaveProductImageAsync(requestFiles[0]);
        }
        var productDto = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Model = product.Model,
            Image = product.ImageUrl
        };
        var savedProduct = await _productService.UpdateProductAsync(productDto);
        if(savedProduct == null)
            return BadRequest();
        
        var productModel = new ProductModel
        {
            Id = savedProduct.Id,
            Name = savedProduct.Name,
            Model = savedProduct.Model,
            ImageUrl = savedProduct.Image
        };
        return Ok(productModel);
    }
    
    [HttpDelete]
    [Route("delete/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var deleted = await _productService.DeleteProductAsync(id);
        if(!deleted)
            return BadRequest();
        return Ok();
    }
    
}