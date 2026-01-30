using Battleships.DTOs;
using Battleships.DTOs.Easy;
using Battleships.Enums;
using Battleships.Services;
using Microsoft.AspNetCore.Mvc;

namespace Battleships.Controllers;

[ApiController]
[Route("[controller]")]
public class SimpleGamesController(ILogger<SimpleGamesController> logger, ISimpleGameService simpleGameService)
{
    private readonly ILogger<SimpleGamesController> _logger = logger;
    private readonly ISimpleGameService _simpleGameService = simpleGameService;

    [HttpPost("games")]
    public async Task<CreatedGameDTO> CreateGame(CreateGameDto joinRequest)
    {
        var match = await _simpleGameService.CreateGame(joinRequest);

        return match;

    }
    
    [HttpGet("{id:Guid}/fireee")]
    public async Task<ActionResult<MoveResult>> Fire([FromRoute] Guid id,[FromBody]FireRequestDto fireRequestDto)
    {
        return await _simpleGameService.Fireee(id,fireRequestDto);

    }
}