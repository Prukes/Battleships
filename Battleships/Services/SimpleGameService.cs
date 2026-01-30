using System.Collections.Concurrent;
using Battleships.DTOs;
using Battleships.DTOs.Easy;
using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Models;
using Battleships.Models.Medium;
using Microsoft.AspNetCore.Mvc;

namespace Battleships.Services;


public interface ISimpleGameService
{
    public Task<CreatedGameDTO> CreateGame(CreateGameDto createdGameDto);
    public Task<MoveResult> Fireee(Guid matchId,FireRequestDto fireRequest);

}
public class SimpleGameService : ISimpleGameService
{
    private readonly ConcurrentDictionary<Guid, GameState> _games = new();
    
    public Task<CreatedGameDTO> CreateGame(CreateGameDto createdGameDto)
    {
        var boardOne = new Board(createdGameDto.BoardSize);
        var boardTwo = new Board(createdGameDto.BoardSize);
        boardOne.GenerateBoard();
        boardTwo.GenerateBoard();
        var pOne = new Player{Name = createdGameDto.PlayerOneName};
        var pTwo = new Player{Name = createdGameDto.PlayerTwoName};
        
        
        var game = new GameState(pOne, pTwo, boardOne, boardTwo);
        
        var gameDto = new CreatedGameDTO();
        _games.TryAdd(gameDto.MatchId, game);

        return Task.FromResult(gameDto);
    }

    public Task<MoveResult> Fireee(Guid matchId, FireRequestDto fireRequest)
    {
        var gameExists = _games.ContainsKey(matchId);
        if(!gameExists) throw new GameNotFoundException("Game not found");
        
        var game = _games[matchId];
        game.HandleMove(fireRequest.PositionX, fireRequest.PositionY);
        return Task.FromResult(new MoveResult());
    }

}