namespace Battleships.Models;

public static class ShipsCollection
{
    public static readonly char[][,] Ships = new char[7][,]
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