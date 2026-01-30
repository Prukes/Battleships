using Battleships.Enums;
using Battleships.Exceptions;

namespace Battleships.Models.Medium;

public class GameState
{
    public GameState(Player pOne, Player pTwo, Board boardOne, Board boardTwo)
    {
        PlayerOne = pOne;
        PlayerTwo = pTwo;
        PlayerOneBoard = boardOne;
        PlayerTwoBoard = boardTwo;
        ActivePlayer = pOne;
        ActiveBoard = boardOne;
    }

    // private const int NumOfBoatTiles = 20;

    private Player PlayerOne { get; }
    private Player PlayerTwo { get; }
    
    //TODO: maybe change to "ActiveBoard" and switch references to boards instead of Players - should have no if checks for player move -> move board moves into Board itself
    private Player ActivePlayer { get; set; }
    private Board ActiveBoard { get; set; }

    public Board PlayerOneBoard { get; }
    public Board PlayerTwoBoard { get; }

    public Player GetPlayerOne() => PlayerOne;
    public Player GetPlayerTwo() => PlayerTwo;
    public Player GetActivePlayer() => ActivePlayer;

    private void SwitchPlayerTurn()
    {
        if (ActivePlayer == PlayerTwo)
        {
            ActivePlayer = PlayerOne;
            ActiveBoard = PlayerOneBoard;
        }
        else
        {
            ActivePlayer = PlayerTwo;
            ActiveBoard = PlayerTwoBoard;
        }
    }

    public MoveResult HandleMove(int x, int y)
    {
        ValidateMove(x, y);

        var result = ActiveBoard.HandleMove(x, y);
        
        if (result == MoveResult.Water)
        {
            SwitchPlayerTurn();
        }
        
        return result;
    }


    private void ValidateMove(int x, int y)
    {
        // Square board (width == height) 
        var maxWidth = PlayerOneBoard.Size;
        if (x < 0 || x > maxWidth)
            throw new ArgumentOutOfRangeException(nameof(x),
                $"X position is out of range (must be between 0 and {maxWidth})");
        if (y < 0 || y > maxWidth)
            throw new ArgumentOutOfRangeException(nameof(x),
                $"Y position is out of range (must be between 0 and {maxWidth})");
    }



}