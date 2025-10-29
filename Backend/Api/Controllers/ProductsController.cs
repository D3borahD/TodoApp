using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService _productService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProductsAsync()
    {
        var products = await _productService.GetProductsAsync();
        
        if (!products.Any() || products.Count == 0)
        {
            return NotFound("No products found");
        };
        
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
    {
        ProductDto? product = await _productService.GetProductsByIdAsync(id);

        if (product == null) return NotFound("Product not found");
       
        return Ok(product);
    }

    [HttpPost("")]
    public async Task<ActionResult<ProductDto>> CreateProductAsync([FromBody] ProductDto productDto)
    {
        if(productDto == null) return BadRequest("The product is null");
        
        ProductDto createdProduct = await _productService.CreateProductAsync(productDto);
        
        if (createdProduct == null) return StatusCode(500, "Error while creating the product.");
        return Ok(createdProduct);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductDto>> UpdateProductAsync(int id, [FromBody] ProductDto productDto)
    {
        if (productDto == null) return BadRequest("The product is null");
        productDto.Id = id;
        
        ProductDto? updatedProduct = await _productService.UpdateProductAsync(productDto);
        
        if (updatedProduct == null) return NotFound($"Product not found with id {id}");
        
        return Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductDto>> DeleteProductAsync(int id)
    {
        bool isDeleted = await _productService.DeleteProductAsync(id);
        
        if (!isDeleted) return NotFound($"Product not found with id {id}");
        
        return NoContent();
    }
    
    
}