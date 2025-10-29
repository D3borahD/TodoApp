using Application.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ModulesController(IBaseService<ModuleDto> moduleService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetModulesAsync()
    {
        var modules = await moduleService.GetAllAsync();
        if (!modules.Any()) return NotFound("No modules found");
        return Ok(modules);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetModulesByIdAsync(int id)
    {
        var moduleDto = await moduleService.GetByIdAsync(id);
        return Ok(moduleDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddModuleAsync([FromBody] ModuleDto? module)
    {
        if (module is null) return BadRequest("Module is null");
        var createdModule = await moduleService.CreateAsync(module);
        return Ok(createdModule);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateModuleAsync(int id, [FromBody] ModuleDto? module)
    {
        if (module is null) return BadRequest("Module is null");
        
        module. Id = id;
        var updated = await moduleService.UpdateAsync(module);
        return updated is null ? NotFound("Module could not be updated") : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteModuleAsync(int id)
    {
        bool isDeleted = await moduleService.DeleteAsync(id);
        return isDeleted ? NoContent() : NotFound("Module not found");
    }
    
}