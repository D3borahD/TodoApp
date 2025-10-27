using Application.Services;
using BackendApi.Models;
using Domain.DTO;
using Infrastructure.IRepository;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using Assert = Xunit.Assert;

namespace Tests;

public class TeamServiceTests
{
    private readonly Mock<ITeamRepository> _repo = new();
    private readonly Mock<ILogger<TeamService>> _logger = new();

    [Fact]
    public async Task CreateTeamsAsync_AssignsNewId_AndReturnsDto()
    {
        // ARRANGE
        // moq équipe 1
        var existing = new List<TeamDao> { new TeamDao() { Id = 1, Label = "Test" } };
        // récupère l'équipe créée
        _repo.Setup(x => x.GetTeamsAsync()).ReturnsAsync(existing);
        // crée la première équipe
        _repo.Setup(x => x.CreateTeamAsync(It.IsAny<TeamDao>())).ReturnsAsync((TeamDao t ) => t );
        
        var teamService = new TeamService(_repo.Object, _logger.Object);
        
        // crée une deuxième équipe
        var dto = new TeamDto{ Label = "New" };
        
        // ACTION
        var created = await teamService.CreateTeamAsync(dto);

        // ASSERT
        Assert.NotNull(created);
        Assert.Equal(2, created.Id);
        Assert.Equal("New", created.Label);
        // vérify que le repo a été appelé exactement 1 fois avec les bons paramètres
        _repo.Verify(x => x.CreateTeamAsync(It.Is<TeamDao>(td => td.Id == 2 && td.Label == "New")), Times.Once);
    }

}