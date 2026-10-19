using Application.DTO;

namespace Application.Interfaces;

public interface IStepService 
{
    Task<List<StepResponseDto>> GetStepsByProjectAsync(int id);
    Task<StepResponseDto?> CreateAsync(StepRequestDto stepDto);
    Task<StepResponseDto?> UpdateAsync(StepResponseDto stepDto);
    Task<bool> DeleteAsync(int stepId);
}