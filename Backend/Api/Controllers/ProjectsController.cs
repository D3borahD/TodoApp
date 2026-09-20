using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController(IBaseService<ProjectDto> baseService, IStepService stepService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<ProjectDto>>> GetProductsAsync()
    {
        var products = await baseService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectDto>> GetProductByIdAsync(int id)
    {
        var product = await baseService.GetByIdAsync(id);
        return Ok(product);
    }
    

    [HttpPost("")]
    public async Task<ActionResult<ProjectDto>> CreateProductAsync([FromBody] ProjectDto? projectDto)
    {
        if(projectDto is null ) return BadRequest("The project is null");
        if(string.IsNullOrEmpty(projectDto.Label)) return BadRequest("The project label is null");
        var createdProject = await baseService.CreateAsync(projectDto);
        return Ok(createdProject);
    }
    
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProjectAsync(int id, [FromBody] ProjectDto? projectDto)
    {
        if (projectDto is null) return BadRequest("The product is null");
        
        projectDto.Id = id;
        var updatedProduct = await baseService.UpdateAsync(projectDto);
        return updatedProduct is null ? NotFound("Module could not be updated") : Ok(updatedProduct);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectDto>> DeleteProductAsync(int id)
    {
        bool isDeleted = await baseService.DeleteAsync(id);
        return isDeleted ? NoContent() : NotFound("Module not found");
    }
    
    
    
    [HttpGet("{id:int}/steps")]
    public async Task<ActionResult<List<StepDto>>> GetStepsByProjectAsync(int id)
    {
        var steps = await stepService.GetStepsByProjectAsync(id);
        return Ok(steps);
    }
    
    [HttpPost("{id:int}/steps")]
    public async Task<ActionResult<StepDto>> CreateStepAsync(int id, [FromBody] StepDto? stepDto)
    {
        if (stepDto is null) return BadRequest("The step is null");
        
        stepDto.ProjectId = id;
        var createdStep = await stepService.CreateAsync(stepDto);
        return Ok(createdStep);
    }
    
    
    
    
    
    [HttpPut("{id:int}/steps/{stepId:int}")]
    public async Task<ActionResult> UpdateStepAsync(int id, [FromBody] StepDto? stepDto)
    {
        if (stepDto is null) return BadRequest("The product is null");
        
        stepDto.Id = id;
        var updatedProduct = await stepService.UpdateAsync(stepDto);
            //return updatedProduct is null ? NotFound("Module could not be updated") : Ok(updatedProduct);
        return NoContent();
    }

   
    

    
    [HttpDelete("{id:int}/steps/{stepId:int}")]
    public async Task<ActionResult> DeleteStepAsync(int id, int stepId)
    {
        bool isDeleted = await stepService.DeleteAsync(stepId);
        return isDeleted ? NoContent() : NotFound("Step not found");
    }
}
