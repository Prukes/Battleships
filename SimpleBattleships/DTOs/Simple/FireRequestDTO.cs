using System.ComponentModel.DataAnnotations;

namespace Battleships.DTOs.Simple;

public class FireRequestDto
{
    [Required]
    public required Guid PlayerId { get; init; }
    [Required]
    [Range(0, 19)]
    public int PositionX { get; init; }
    [Required]
    [Range(0, 19)]
    public int PositionY { get; init; }
    
}