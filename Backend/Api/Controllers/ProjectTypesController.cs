using Application.DTO;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ProjectTypesController(IBaseService<ProjectTypeDto> baseService) : ControllerBase
{
     
    [HttpGet("")]
    public async Task<ActionResult<List<ProjectTypeDto>>> GetProjectTypeAsync()
    {
        var projectTypeList = await baseService.GetAllAsync();
        return Ok(projectTypeList);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectTypeDto>> GetProjectTypeByIdAsync(int id)
    {
        var projectType = await baseService.GetByIdAsync(id);
        return Ok(projectType);
    }
    
    [HttpPost("")]
    public async Task<ActionResult<ProjectTypeDto>> CreateProjectTypeAsync(ProjectTypeDto projectTypeDto)
    {
        var createdProjectType = await baseService.CreateAsync(projectTypeDto);
        return Ok(createdProjectType);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProjectTypeDto>> UpdateProjectTypeAsync(int id, [FromBody]  ProjectTypeDto? projectTypeDto)
    {
        if(projectTypeDto is null) return BadRequest("The project type is null");
        
        projectTypeDto.Id = id;
        var updatedProjectType = await baseService.UpdateAsync(projectTypeDto);
        return updatedProjectType is null ? NotFound("project type could not be updated") : Ok(updatedProjectType);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProjectTypeDto>> DeleteProjectTypeAsync(int id)
    {
        bool isDeleted = await baseService.DeleteAsync(id);
        return isDeleted ? NoContent() : NotFound("project type not found");
    }
    
}