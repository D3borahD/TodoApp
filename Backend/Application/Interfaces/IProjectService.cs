using Application.DTO;

namespace Application.Interfaces;

public interface IProjectService
{
    Task<bool> DeleteAsync(int id);
    Task<ProjectResponseDto?> UpdateAsync(ProjectRequestDto projectDto);
    Task<ProjectResponseDto?> CreateAsync(ProjectRequestDto projectDto);
    Task<ProjectResponseDto?> GetByIdAsync(int id);
    Task<List<ProjectResponseDto?>> GetAllAsync();
}