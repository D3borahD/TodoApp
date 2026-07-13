using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ProjectTypesController(IBaseService<ProjectTypesDto> baseService) : ControllerBase
{
     
    [HttpGet("")]
    public async Task<ActionResult<List<ProjectTypesDto>>> GetProjectTypeAsync()
    {
        var projectTypeList = await baseService.GetAllAsync();
        return Ok(projectTypeList);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProjectTypesDto>> GetProjectTypeByIdAsync(int id)
    {
        var projectType = await baseService.GetByIdAsync(id);
        return Ok(projectType);
    }
    
    [HttpPost("")]
    public async Task<ActionResult<ProjectTypesDto>> CreateProjectTypeAsync(ProjectTypesDto projectTypeDto)
    {
        var createdProjectType = await baseService.CreateAsync(projectTypeDto);
        return Ok(createdProjectType);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProjectTypesDto>> UpdateProjectTypeAsync(int id, [FromBody]  ProjectTypesDto? projectTypeDto)
    {
        if(projectTypeDto is null) return BadRequest("The project type is null");
        
        projectTypeDto.Id = id;
        var updatedProjectType = await baseService.UpdateAsync(projectTypeDto);
        return updatedProjectType is null ? NotFound("project type could not be updated") : Ok(updatedProjectType);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProjectTypesDto>> DeleteProjectTypeAsync(int id)
    {
        bool isDeleted = await baseService.DeleteAsync(id);
        return isDeleted ? NoContent() : NotFound("project type not found");
    }
    
}