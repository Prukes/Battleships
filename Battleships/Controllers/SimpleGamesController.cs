using Battleships.DTOs;
using Battleships.DTOs.Easy;
using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Battleships.Controllers;

[ApiController]
[Route("[controller]")]
public class SimpleGamesController(ILogger<SimpleGamesController> logger, ISimpleGameService simpleGameService):ControllerBase
{
    private readonly ILogger<SimpleGamesController> _logger = logger;
    private readonly ISimpleGameService _simpleGameService = simpleGameService;

    [HttpPost("games")]
    public async Task<CreatedGameDto> CreateGame(CreateGameDto joinRequest)
    {
        var match = await _simpleGameService.CreateGame(joinRequest);

        return match;

    }
    
    [HttpGet("{matchId:Guid}/fireee")]
    public async Task<IActionResult> Fire([FromRoute] Guid matchId,[FromQuery]FireRequestDto fireRequestDto)
    {
        try
        {
            var result = await _simpleGameService.Fireee(matchId, fireRequestDto);
            return Ok(result);
        }
        catch (GameNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (DuplicateAttackException e)
        {
           return Conflict(e.Message);
        }

    }
}