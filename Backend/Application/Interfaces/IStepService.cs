using Domain.DTO;

namespace Application.Interfaces;

public interface IStepService : IBaseService<StepDto>
{
    Task<List<StepDto>> GetStepsByProjectAsync(int id);
}