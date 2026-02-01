using Battleships.DTOs;
using Battleships.DTOs.Easy;
using Battleships.DTOs.Simple;
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
    [HttpPost("games")]
    public async Task<CreatedGameDto> CreateGame(CreateGameDto joinRequest)
    {
        logger.LogInformation("Creating a new game");
        var match = await simpleGameService.CreateGame(joinRequest);
        logger.LogInformation("Game created with id: {matchId}", match.MatchId);
        return match;

    }
    
    [HttpGet("{matchId:Guid}/fireee")]
    public async Task<IActionResult> Fire([FromRoute] Guid matchId,[FromQuery]FireRequestDto fireRequestDto)
    {
        logger.LogInformation("Requested a fire action by {player} in match {matchId} to position x:{x} y:{y}",fireRequestDto.PlayerId, matchId, fireRequestDto.PositionX, fireRequestDto.PositionY);
        try
        {
            var result = await simpleGameService.Fireee(matchId, fireRequestDto);
            logger.LogInformation("Game fired with id: {matchId}", matchId);
            return Ok(result);
        }
        catch (GameNotFoundException e)
        {
            logger.LogError("Game {matchId} encountered an error! {message}", matchId,e.Message);
            return NotFound(e.Message);
        }
        catch (DuplicateAttackException e)
        {
            logger.LogError("Game {matchId} encountered an error! {message}", matchId,e.Message);
            return Conflict(e.Message);
        }
        catch (GameConcludedException e)
        {
            //Hacky way to work around ASP.NETs exception about forgetting to add AddAuthentication() -- Request/Response pipeline
            logger.LogError("Game {matchId} encountered an error! {message}", matchId,e.Message);
            return StatusCode(403,e.Message);
        }
        catch (Exception e)
        {
            logger.LogError("I'm a teapot! Game {matchId} encountered an error! {message}", matchId,e.Message);
            return StatusCode(418,e.Message);
        }
        

    }
}