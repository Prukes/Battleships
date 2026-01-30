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
        PlayerToMove = pOne;
    }

    private const int NumOfBoatTiles = 20;

    private Player PlayerOne { get; }
    private Player PlayerTwo { get; }
    
    //TODO: maybe change to "ActiveBoard" and switch references to boards instead of Players - should have no if checks for player move -> move board moves into Board itself
    private Player PlayerToMove { get; set; }

    private Board PlayerOneBoard { get; }
    private Board PlayerTwoBoard { get; }

    public Player GetPlayerOne() => PlayerOne;
    public Player GetPlayerTwo() => PlayerTwo;
    public Player GetPlayerToMove() => PlayerToMove;

    private void SwitchPlayerTurn()
    {
        if (PlayerToMove == PlayerTwo)
        {
            PlayerToMove = PlayerOne;
        }
        else
        {
            PlayerToMove = PlayerTwo;
        }
    }

    public MoveResult HandleMove(int x, int y)
    {
        ValidateMove(x, y);

        if (PlayerToMove == PlayerOne)
        {
            return CheckTile(PlayerOneBoard, x, y);
        }
        else
        {
            return CheckTile(PlayerTwoBoard, x, y);
        }
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

    private MoveResult CheckTile(Board board, int x, int y)
    {
        // Guard clause - cant hit the same tile again
        if (board.GetTile(x, y) == 'U' || board.GetTile(x,y) == 'x') throw new DuplicateAttackException("This tile was already attacked! Move again.");
        //Hit
        if (board.GetTile(x, y) == 'X')
        {
            board.ApplyMove(x, y, 'x');
            // if (board.CheckIfShipSank())
            // {
            //     return MoveResult.Sunk;
            // }
            return MoveResult.Hit;
        }
        else
        {
            // U for used
            board.ApplyMove(x, y, 'U');
            SwitchPlayerTurn();
            return MoveResult.Water;
        }
    }


}