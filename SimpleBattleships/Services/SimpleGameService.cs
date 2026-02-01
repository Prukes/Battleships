using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Battleships.DTOs;
using Battleships.DTOs.Easy;
using Battleships.DTOs.Simple;
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
    public void CleanupFinishedGames();
}

public class SimpleGameService : ISimpleGameService
{
    private readonly ConcurrentDictionary<Guid, GameState> _games = new();

    public Task<CreatedGameDto> CreateGame(CreateGameDto createdGameDto)
    {
        var boardOne = new Board(createdGameDto.BoardSize);
        var boardTwo = new Board(createdGameDto.BoardSize);
        var pOne = new Player(createdGameDto.PlayerOneName);
        var pTwo = new Player(createdGameDto.PlayerTwoName );


        var game = new GameState(pOne, pTwo, boardOne, boardTwo);
        var matchId = Guid.NewGuid();
        _games.TryAdd(matchId, game);

        return Task.FromResult(new CreatedGameDto(matchId, game.PlayerOneBoard.Tiles, game.PlayerTwoBoard.Tiles,pOne.PlayerId, pTwo.PlayerId));
    }

    public Task<GameStateDto> Fireee(Guid matchId, FireRequestDto fireRequest)
    {
        var gameExists = _games.TryGetValue(matchId, out var game);
        if (!gameExists) throw new GameNotFoundException("Game not found");

        var moveBy = game!.GetActivePlayer();
        
        if(!moveBy.PlayerId.Equals(fireRequest.PlayerId))throw new NotYourTurnException("Wait until it's your turn homeboy.");

        var moveResult = game.HandleMove(fireRequest.PositionX, fireRequest.PositionY);
        var gameStateDto = new GameStateDto(moveBy: moveBy,moveResultEnum: moveResult.MoveResultEnum,hasWon: moveResult.HasWon);
        return Task.FromResult(gameStateDto);
    }

    public void CleanupFinishedGames()
    {
        foreach (var game in _games)
        {
            if (game.Value.HasConcluded)
            {
                _games.TryRemove(game);
            }
        }
    }
}