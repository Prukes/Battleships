using System.Collections.Concurrent;
using Battleships.DTOs;
using Battleships.DTOs.Easy;
using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Models;
using Battleships.Models.Medium;
using Microsoft.AspNetCore.Mvc;
using GameStateDto = Battleships.DTOs.Easy.GameStateDto;

namespace Battleships.Services;

public interface ISimpleGameService
{
    public Task<CreatedGameDto> CreateGame(CreateGameDto createdGameDto);
    //ustřelený jméno abych si zachoval sanity
    public Task<GameStateDto> Fireee(Guid matchId, FireRequestDto fireRequest);
}

public class SimpleGameService : ISimpleGameService
{
    private readonly ConcurrentDictionary<Guid, GameState> _games = new();

    public Task<CreatedGameDto> CreateGame(CreateGameDto createdGameDto)
    {
        var boardOne = new Board(createdGameDto.BoardSize);
        var boardTwo = new Board(createdGameDto.BoardSize);
        var pOne = new Player { Name = createdGameDto.PlayerOneName };
        var pTwo = new Player { Name = createdGameDto.PlayerTwoName };


        var game = new GameState(pOne, pTwo, boardOne, boardTwo);
        var matchId = Guid.NewGuid();
        _games.TryAdd(matchId, game);

        return Task.FromResult(new CreatedGameDto(matchId, game.PlayerOneBoard.Tiles, game.PlayerTwoBoard.Tiles));
    }

    public Task<GameStateDto> Fireee(Guid matchId, FireRequestDto fireRequest)
    {
        var gameExists = _games.ContainsKey(matchId);
        if (!gameExists) throw new GameNotFoundException("Game not found");

        var game = _games[matchId];
        var moveBy = game.GetActivePlayer();
        
        if(!moveBy.Name.Equals(fireRequest.PlayerId))throw new NotYourTurnException("Wait till it's your turn homeboy.");

        var moveResult = game.HandleMove(fireRequest.PositionX, fireRequest.PositionY);
        var gameStateDto = new GameStateDto(moveBy: moveBy,moveResultEnum: moveResult.MoveResultEnum,hasWon: moveResult.HasWon);
        return Task.FromResult(gameStateDto);
    }
}