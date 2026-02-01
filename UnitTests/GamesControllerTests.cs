using Battleships.Controllers;
using Battleships.DTOs.Easy;
using Battleships.DTOs.Simple;
using Battleships.Exceptions;
using Battleships.Models;
using Battleships.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTests;

public class GamesControllerTests
{
    [Fact]
    public async Task CreateGame_CreatesNewGame()
    {
        var mockService = new Mock<ISimpleGameService>();
        var mockLogger = new Mock<ILogger<SimpleGamesController>>();
        var createGameRequest = new CreateGameDto {BoardSize = 15,PlayerOneName = "A", PlayerTwoName = "B"};
        
        var matchId = Guid.NewGuid();
        var pOne = new Player(createGameRequest.PlayerOneName);
        var pTwo = new Player(createGameRequest.PlayerTwoName);
        var pOneBoard = new Board(createGameRequest.BoardSize);
        var pTwoBoard = new Board(createGameRequest.BoardSize);

        var expectedDto =
            new CreatedGameDto(matchId, pOneBoard.Tiles, pTwoBoard.Tiles, pOne.PlayerId, pTwo.PlayerId);

        // Tell the mock what to do when CreateGame is called
        mockService
            .Setup(s => s.CreateGame(createGameRequest))
            .ReturnsAsync(new CreatedGameDto(matchId, pOneBoard.Tiles, pTwoBoard.Tiles, pOne.PlayerId,pTwo.PlayerId));

        var controller = new SimpleGamesController(mockLogger.Object,mockService.Object);

        // 2. Act
        var actualDto = await controller.CreateGame(createGameRequest);

        // 3. Assert: Verify the HTTP status code
        Assert.IsType<CreatedGameDto>(actualDto);
        //Can't really test expectedDto vs actualDto because controller.CreateGame creates new random boards.
    }
    

    [Fact]
    public async Task Fire_WhenDuplicateAttack_ReturnsConflict()
    {
        // 1. Arrange: Create the Mock
        var mockService = new Mock<ISimpleGameService>();
        var mockLogger = new Mock<ILogger<SimpleGamesController>>();
        var matchId = Guid.NewGuid();
        var request = new FireRequestDto { PlayerId = Guid.NewGuid(),PositionX = 1,PositionY = 2};

        // Tell the mock what to do when Fireee is called
        mockService
            .Setup(s => s.Fireee(matchId, It.IsAny<FireRequestDto>()))
            .ThrowsAsync(new DuplicateAttackException("Already hit!"));

        var controller = new SimpleGamesController(mockLogger.Object,mockService.Object);

        // 2. Act
        var result = await controller.Fire(matchId, request);

        // 3. Assert: Verify the HTTP status code
        var conflictResult = Assert.IsType<ConflictObjectResult>(result);
        Assert.Equal(409, conflictResult.StatusCode);
    }
    
    [Fact]
    public async Task Fire_WhenNonexistentMatchId_ReturnsNotFound()
    {
        // 1. Arrange: Create the Mock
        var mockService = new Mock<ISimpleGameService>();
        var mockLogger = new Mock<ILogger<SimpleGamesController>>();
        var matchId = Guid.NewGuid();
        var request = new FireRequestDto { PlayerId = Guid.NewGuid(),PositionX = 1,PositionY = 2};

        // Tell the mock what to do when Fireee is called
        mockService
            .Setup(s => s.Fireee(matchId, It.IsAny<FireRequestDto>()))
            .ThrowsAsync(new GameNotFoundException("Game not found!"));

        var controller = new SimpleGamesController(mockLogger.Object,mockService.Object);

        // 2. Act
        var result = await controller.Fire(matchId, request);

        // 3. Assert: Verify the HTTP status code
        var conflictResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, conflictResult.StatusCode);
    }
}