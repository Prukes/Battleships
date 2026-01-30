using System.ComponentModel.DataAnnotations;

namespace Battleships.DTOs.Easy;

public class CreatedGameDTO
{
    public CreatedGameDTO()
    {
        MatchId = new Guid();
    }
    [Required]
    public Guid MatchId { get; set; }
}