using System.Reflection;
using Application.Interfaces;
using BackendApi.Entities;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers;

[Produces("application/json")]
[ApiController]
[Route("api/[controller]")]
public class ModulesController(IModuleService _moduleService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetModulesAsync()
    {
        var modules = await _moduleService.GetModulesAsync();
        
        if (!modules.Any() ||  modules.Count == 0) return NotFound("No modules found");
        
        return Ok(modules);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetModulesByIdAsync(int id)
    {
        ModuleDto? moduleDto = await _moduleService.GetModulesByIdAsync(id);
        if (moduleDto is null) return NotFound("Module not found");
        return Ok(moduleDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddModuleAsync([FromBody] ModuleDto module)
    {
        if (module is null) return BadRequest("Module is null");
        
        ModuleDto createdModule = await _moduleService.CreateModuleAsync(module);
        
        if (createdModule is null) return BadRequest("Module could not be created");
        
        return Ok(createdModule);
        
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateModuleAsync(int id, [FromBody] ModuleDto module)
    {
        if (module is null) return BadRequest("Module is null");
        module. Id = id;
        ModuleDto? updatedModule = await _moduleService.UpdateModuleAsync(module);
        if (updatedModule is null) return BadRequest("Module could not be updated");
        return Ok(updatedModule);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteModuleAsync(int id)
    {
        bool isDeleted = await _moduleService.DeleteModuleAsync(id);
        
        if (!isDeleted) return BadRequest("Module could not be deleted");
        return NoContent();
    }
    
}