using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ProductsController(IBaseService<ProductDto> baseService, IProductService productService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProductsAsync()
    {
        var products = await baseService.GetAllAsync();
        if (!products.Any()) return NotFound("No products found");
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> GetByIdAsync(int id)
    {
        var product = await baseService.GetByIdAsync(id);
        return Ok(product);
    }
    
    [HttpGet("{id:int}/module")]
    public async Task<ActionResult<List<ModuleDto>>> GetModuleByProductAsync(int id)
    {
        var module = await productService.GetModuleByProductAsync(id);
        return Ok(module);
    }


    [HttpPost("")]
    public async Task<ActionResult<ProductDto>> CreateAsync([FromBody] ProductDto? productDto)
    {
        if(productDto is null) return BadRequest("The product is null");
        var createdProduct = await baseService.CreateAsync(productDto);
        return Ok(createdProduct);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] ProductDto? productDto)
    {
        if (productDto is null) return BadRequest("The product is null");
        
        productDto.Id = id;
        var updatedProduct = await baseService.UpdateAsync(productDto);
        return updatedProduct is null ? NotFound("Module could not be updated") : Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProductDto>> DeleteAsync(int id)
    {
        bool isDeleted = await baseService.DeleteAsync(id);
        return isDeleted ? NoContent() : NotFound("Module not found");
    }
    
    
    
}