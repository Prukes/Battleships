using Battleships.Enums;
using Battleships.Exceptions;

namespace Battleships.Models;

public class GameState
{
    public GameState(Player pOne, Player pTwo, Board boardOne, Board boardTwo)
    {
        PlayerOne = pOne;
        PlayerTwo = pTwo;
        PlayerOneBoard = boardOne;
        PlayerTwoBoard = boardTwo;
        ActivePlayer = pOne;
        ActiveBoard = boardTwo;
        _hasConcluded = false;
    }

    private Player PlayerOne { get; }
    private Player PlayerTwo { get; }
    
    private Player ActivePlayer { get; set; }
    private Board ActiveBoard { get; set; }

    public Board PlayerOneBoard { get; }
    public Board PlayerTwoBoard { get; }

    public Player GetPlayerOne() => PlayerOne;
    public Player GetPlayerTwo() => PlayerTwo;
    public Player GetActivePlayer() => ActivePlayer;
    
    private bool _hasConcluded;
    public bool HasConcluded => _hasConcluded;

    private void SwitchPlayerTurn()
    {
        if (ActivePlayer == PlayerTwo)
        {
            ActivePlayer = PlayerOne;
            ActiveBoard = PlayerTwoBoard;
        }
        else
        {
            ActivePlayer = PlayerTwo;
            ActiveBoard = PlayerOneBoard;
        }
    }

    public MoveResult HandleMove(int x, int y)
    {
        if (_hasConcluded) throw new GameConcludedException($"Game has concluded! Player {ActivePlayer.Name} has won!");
        ValidateMove(x, y);

        var result = ActiveBoard.HandleMove(x, y);
        
        if (result.MoveResultEnum == MoveResultEnum.Water)
        {
            SwitchPlayerTurn();
        }
        else
        {
            if (result.HasWon)
            {
                _hasConcluded = true;
            }
        }

        return result;
    }


    private void ValidateMove(int x, int y)
    {
        // Square board (width == height) 
        var maxWidth = PlayerOneBoard.Size-1;
        if (x < 0 || x > maxWidth)
            throw new ArgumentOutOfRangeException(nameof(x),
                $"X position is out of range (must be between 0 and {maxWidth})");
        if (y < 0 || y > maxWidth)
            throw new ArgumentOutOfRangeException(nameof(x),
                $"Y position is out of range (must be between 0 and {maxWidth})");
    }



}