using System.ComponentModel.DataAnnotations;

namespace Battleships.DTOs.Easy;

public class FireRequestDto
{
    [Required]
    public required string PlayerId { get; set; }
    [Required]
    [Range(0, 19)]
    public int PositionX { get; set; }
    [Required]
    [Range(0, 19)]
    public int PositionY { get; set; }
    
}