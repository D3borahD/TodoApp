using Application.DTO;

namespace Application.Interfaces;

public interface IProjectService
{
    Task<bool> DeleteAsync(int id);
    Task<ProjectResponseDto?> UpdateAsync(ProjectResponseDto projectDto);
    Task<ProjectResponseDto?> CreateAsync(ProjectResponseDto projectDto);
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<List<ProjectResponseDto?>> GetAllAsync();
}