using System.ComponentModel.DataAnnotations;

namespace Battleships.DTOs.Easy;

public class CreateGameDto
{
    [Required] public required string PlayerOneName { get; set; }
    [Required] public required string PlayerTwoName { get; set; }
    [Range(10,20)]
    public required int BoardSize { get; set; }
}