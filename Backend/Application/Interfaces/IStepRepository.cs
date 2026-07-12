using Domain.Entities;

namespace Application.Interfaces;

public interface IStepRepository : IBaseRepository<StepDao>
{
    public Task<List<StepDao>> GetStepByProjectAsync(int projectId);
}