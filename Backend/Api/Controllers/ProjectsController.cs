using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IBaseService<ProjectDto> baseService, IProjectService projectService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetProductsAsync()
    {
        var products = await baseService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDto>> GetByIdAsync(int id)
    {
        var product = await baseService.GetByIdAsync(id);
        return Ok(product);
    }
    

    [HttpPost("")]
    public async Task<ActionResult<ProjectDto>> CreateAsync([FromBody] ProjectDto? projectDto)
    {
        if(projectDto is null ) return BadRequest("The project is null");
        if(string.IsNullOrEmpty(projectDto.Label)) return BadRequest("The project label is null");
        var createdProject = await baseService.CreateAsync(projectDto);
        return Ok(createdProject);
    }

    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateAsync(int id, [FromBody] ProjectDto? projectDto)
    {
        if (projectDto is null) return BadRequest("The product is null");
        
        projectDto.Id = id;
        var updatedProduct = await baseService.UpdateAsync(projectDto);
        return updatedProduct is null ? NotFound("Module could not be updated") : Ok(updatedProduct);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectDto>> DeleteAsync(int id)
    {
        bool isDeleted = await baseService.DeleteAsync(id);
        return isDeleted ? NoContent() : NotFound("Module not found");
    }
    
    
    
    
}
