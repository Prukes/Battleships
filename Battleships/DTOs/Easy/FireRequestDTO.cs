using System.ComponentModel.DataAnnotations;

namespace Battleships.DTOs.Easy;

public class FireRequestDto
{
    [Required]
    public Guid PlayerId { get; set; }
    [Required]
    [Range(0, 20)]
    public int PositionX { get; set; }
    [Required]
    [Range(0, 20)]
    public int PositionY { get; set; }
    
}