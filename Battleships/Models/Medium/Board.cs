namespace Battleships.Models;

public class Board
{
    public Board(int size)
    {
        Size = size;
        Tiles = new char[size, size];
    }

    public int Size { get; set; }
    private char[,] Tiles { get; set; }

    public char GetTile(int x, int y) => Tiles[x, y];

    public void GenerateBoard()
    {
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

        // 1x The T-Shape (3x3 grid)
        new char[,]
        {
            { 'W', 'X', 'W' },
            { 'X', 'X', 'X' },
            { 'W', 'X', 'W' }
        },

        // 1x The L-Shape (2x5 grid)
        new char[,]
        {
            { 'W', 'W', 'X', 'W', 'W' },
            { 'X', 'X', 'X', 'X', 'X' },
            { 'W', 'W', 'X', 'W', 'W' }
        }
    };
}