using System.ComponentModel.DataAnnotations;

namespace Battleships.DTOs.Easy;

public class CreatedGameDto
{
    [Required]
    public Guid MatchId { get; set; }

    
    //Sending Boards only for development/testing purposes.
    //In prod this is shouldn't exist purely for the logic
    //of not seeing the game state of other player.
    [Required] public char[,] Board1 { get; set; }
    [Required] public char[,] Board2 { get; set; }

    public CreatedGameDto(Guid matchId, char[,] board1, char[,] board2)
    {
        MatchId = matchId;
        Board1 = board1;
        Board2 = board2;
    }
    
}