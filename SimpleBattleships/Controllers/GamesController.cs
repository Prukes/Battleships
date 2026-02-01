using Battleships.DTOs;
using Battleships.DTOs.Medium;
using Battleships.Services;
using Microsoft.AspNetCore.Mvc;

namespace Battleships.Controllers;

[ApiController]
[Route("[controller]")]
public class GamesController : ControllerBase
{

    private readonly ILogger<GamesController> _logger;
    private readonly IGameService _gameService;

    public GamesController(ILogger<GamesController> logger,  IGameService gameService)
    {
        _gameService = gameService;
        _logger = logger;
    }

    [HttpGet("{id:Guid}")]
    public async Task<ActionResult<GameStateDto>> GetGameState(Guid id) 
    {
        _logger.LogInformation($"Getting game state for {id}");
        var match = await _gameService.GetMatch(id);
        if (match == null)
        {
            _logger.LogError($"Getting game state for {id} failed");
            return NotFound(match);
        }
        _logger.LogInformation($"Getting game state for {id} succeeded");
        return Ok(match);
    }

    [HttpPost("/queue/join")]
    public Task<ActionResult<JoinResponseDto>> JoinQueue(JoinRequestDto joinRequest)
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("/queue/{playerId:Guid}/check")]
    public Task<ActionResult<Guid>> JoinQueue()
    {
        throw new NotImplementedException();
    }
        


}