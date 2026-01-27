namespace Battleships.Models;

public class Board(int size)
{
    public char[,] Tiles { get; set; } =  new char[size,size];
}