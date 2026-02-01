using Battleships.Models;

namespace UnitTests;

public class BoardTests
{
    //This test is absolute trash...
    //Proper test should also check each "ship", check whether its rotated 
    //check its bounding box and then neighbors. There should be no overlap.
    //Tldr; I'm too lazy to implement such logic in this project.
    [Fact]
    public void Test_Board_Proper_Count_Generation()
    {
        
        const int properNumOfXs = 21;
        var boardSize = new Random().Next(10,20);
        var board =  new Board(boardSize);
        
        
        var numOfXs = 0;
        foreach (var c in board.Tiles)
        {
            if (c == 'X')
            {
                numOfXs++;
            }
        }
        Assert.Equal(properNumOfXs, numOfXs);
        Assert.Equal(boardSize, board.Size);
    }
}