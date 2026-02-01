using Battleships.Controllers;
using Battleships.Models;
using Battleships.Services;
using Microsoft.AspNetCore.SignalR;
using SignalRSwaggerGen.Attributes;

namespace Battleships.Hubs;
[SignalRHub("/gamehub")]
public class GameHub(ILogger<GameHub> logger, IGameService gameService) : Hub
{
    /// Hokus pokus na potom...
    // private readonly ILogger<GameHub> _logger = logger;
    // private readonly IGameService _gameService = gameService;
    //
    // public async Task FindMatch(string playerName)
    // {
    //     var player = new Player { ConnectionId = Context.ConnectionId, Name = playerName };
    //     var result = await _gameService.JoinQueue(player);
    //
    //     if (result != null)
    //     {
    //         // Create a SignalR group for this specific match
    //         await Groups.AddToGroupAsync(result.Player1.ConnectionId, result.MatchId.ToString());
    //         await Groups.AddToGroupAsync(result.Player2.ConnectionId, result.MatchId.ToString());
    //
    //         // Alert both players that the game has started
    //         await Clients.Group(result.MatchId.ToString()).SendAsync("StartGame", new { 
    //             matchId = result.MatchId, 
    //             opponent = playerName 
    //         });
    //     }
    //     else
    //     {
    //         await Clients.Caller.SendAsync("WaitingInQueue");
    //     }
    // }
}