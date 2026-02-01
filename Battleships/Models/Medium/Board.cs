using Battleships.Enums;
using Battleships.Exceptions;
using Battleships.Models.Medium;

namespace Battleships.Models;

public class Board
{
    public Board(int size)
    {
        Size = size;
        Tiles = new char[size, size];
        _rng = new Random();
        NumOfHits = 0;
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                Tiles[i, j] = 'W'; 
            }
        }
        GenerateBoard();
    }

    public int Size { get; set; }
    public char[,] Tiles { get; }
    
    private readonly Random _rng;

    private const int NumOfShipSlots = 21;
    private int NumOfHits { get; set; }

    private char GetTile(int x, int y) => Tiles[x, y];

    private void GenerateBoard()
    {
        foreach (var ship in _ships.Reverse()) {
            var placed = false;
            var attempts = 0;

            while (!placed && attempts < 100) {
                var startY = _rng.Next(0, Tiles.GetLength(0));
                var startX = _rng.Next(0, Tiles.GetLength(1));
                var rotated = _rng.NextDouble() >= 0.5;

                if (CanPlace(ship, startX, startY,rotated)) {
                    AddShip(ship, startX, startY, rotated);
                    placed = true;
                }

                attempts++;
            }
        }
    }
    private bool CanPlace(char[,] ship, int startY, int startX, bool rotated)
    {
        // If rotated, the width of the array becomes the height on the board
        var shipRows = ship.GetLength(0);
        var shipCols = ship.GetLength(1);
    
        var visualRows = rotated ? shipCols : shipRows;
        var visualCols = rotated ? shipRows : shipCols;

        // 1. Boundary Check
        if (startY + visualRows > Tiles.GetUpperBound(1) || startX + visualCols > Tiles.GetUpperBound(0))
            return false;

        for (var i = 0; i < shipRows; i++)
        {
            for (var j = 0; j < shipCols; j++)
            {
                if (ship[i, j] != 'X') continue;
                // Map the ship's (i, j) to board coordinates (r, c)
                var r = rotated ? j : i;
                var c = rotated ? i : j;
                
                var targetY = startY + r;
                var targetX = startX + c;

                // 2. Diagonal/Neighborhood Check (-1 to +1)
                for (var ny = -1; ny <= 1; ny++)
                {
                    for (var nx = -1; nx <= 1; nx++)
                    {
                        var checkY = targetY + ny;
                        var checkX = targetX + nx;

                        if (checkY >= 0 && checkY <= Tiles.GetUpperBound(1) && 
                            checkX >= 0 && checkX <= Tiles.GetUpperBound(0))
                        {
                            if (GetTile(checkY, checkX) == 'X') return false;
                        }
                    }
                }
            }
        }
        return true;
    }
    
    private void AddShip(char[,] ship, int startX, int startY, bool rotated)
    {
        for (var i = 0; i < ship.GetLength(0); i++)
        {
            for (var j = 0; j < ship.GetLength(1); j++)
            {
                if (ship[i, j] == 'X')
                {
                    var r = rotated ? j : i;
                    var c = rotated ? i : j;
                    ApplyMove(startX+r,startY+c,'X');
                }
            }
        }
    }

    public MoveResult HandleMove(int x, int y)
    {
        return CheckTile(x, y);
    }

    private void ApplyMove(int x, int y, char result)
    {
        Tiles[x, y] = result;
    }

    private MoveResult CheckTile( int x, int y)
    {
        // Guard clause - can't hit the same tile again
        if (GetTile(x, y) == 'U' || GetTile(x,y) == 'x') throw new DuplicateAttackException("This tile was already attacked! Move again.");
        //Hit
        if (GetTile(x, y) == 'X')
        {
            ApplyMove(x, y, 'x');
            //I'm lazy to implement BFS to check if ship has sank... for now of course
            // if (board.CheckIfShipSank())
            // {
            //     return MoveResult.Sunk;
            // }
            NumOfHits++;
            return new MoveResult(MoveResultEnum.Hit, NumOfHits == NumOfShipSlots);
        }
        else
        {
            // U for used
            ApplyMove(x, y, 'U');
            return new MoveResult( MoveResultEnum.Water, NumOfHits == NumOfShipSlots );
        }
    }

    private char[][,] _ships = new char[7][,]
    {
        // 2x Single Block [X]
        new char[,] { { 'X' } },
        new char[,] { { 'X' } },

        // 2x Double Block [XX]
        new char[,] { { 'X', 'X' } },
        new char[,] { { 'X', 'X' } },

        // 1x Triple Block [XXX]
        new char[,] { { 'X', 'X', 'X' } },

        // 1x The Plus-Shape (3x3 grid)
        new char[,]
        {
            { 'W', 'X', 'W' },
            { 'X', 'X', 'X' },
            { 'W', 'X', 'W' }
        },

        // 1x The Plus-Sized-Plus-Shape (3x5 grid)
        new char[,]
        {
            { 'W', 'W', 'X', 'W', 'W' },
            { 'X', 'X', 'X', 'X', 'X' },
            { 'W', 'W', 'X', 'W', 'W' }
        }
    };
}