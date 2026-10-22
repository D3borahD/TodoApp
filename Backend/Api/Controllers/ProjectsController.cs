using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController(
    IProjectService projectsService, 
    IStepService stepService) 
    : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<List<ProjectResponseDto>>> GetProjectAsync()
    {
        var products = await projectsService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectResponseDto>> GetProjectByIdAsync(int id)
    {
        var product = await projectsService.GetByIdAsync(id);
        return Ok(product);
    }
    
    [HttpPost]
    public async Task<ActionResult<ProjectResponseDto>> CreateProjectAsync([FromBody] ProjectRequestDto? projectDto)
    {
        if(projectDto is null ) return BadRequest("The project is null");
        if(string.IsNullOrEmpty(projectDto.Label)) return BadRequest("The project label is null");
        var createdProject = await projectsService.CreateAsync(projectDto);
        return Ok(createdProject);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProjectAsync(int id, [FromBody] ProjectRequestDto? projectDto)
    {
        if (projectDto is null) return BadRequest("The product is null");
        
        projectDto.Id = id;
        var updatedProduct = await projectsService.UpdateAsync(projectDto);
        return updatedProduct is null ? NotFound("Module could not be updated") : Ok(updatedProduct);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProjectResponseDto>> DeleteProjectAsync(int id)
    {
        bool isDeleted = await projectsService.DeleteAsync(id);
        return isDeleted ? NoContent() : NotFound("Project not found");
    }
    
    [HttpGet("{id:int}/steps")]
    public async Task<ActionResult<List<StepResponseDto>>> GetStepsByProjectAsync(int id)
    {
        var steps = await stepService.GetStepsByProjectAsync(id);
        return Ok(steps);
    }
    
    [HttpPost("{id:int}/steps")]
    public async Task<ActionResult<StepResponseDto>> CreateStepAsync(int id, [FromBody] StepRequestDto? stepDto)
    {
        if (stepDto is null) return BadRequest("The step is null");
        
        stepDto.ProjectId = id;
        var createdStep = await stepService.CreateAsync(stepDto);
        return Ok(createdStep);
    }
    
    
    [HttpPut("{id:int}/steps/{stepId:int}")]
    public async Task<ActionResult> UpdateStepAsync(int id, [FromBody] StepResponseDto? stepDto)
    {
        if (stepDto is null) return BadRequest("The product is null");
        
        var updatedStep = await stepService.UpdateAsync(stepDto);
        return updatedStep is null ? NotFound("Module could not be updated") : Ok(updatedStep);
    }
    
    [HttpDelete("{id:int}/steps/{stepId:int}")]
    public async Task<ActionResult> DeleteStepAsync(int id, int stepId)
    {
        bool isDeleted = await stepService.DeleteAsync(stepId);
        return isDeleted ? NoContent() : NotFound("Step not found");
    }
}
