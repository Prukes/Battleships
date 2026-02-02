using System.ComponentModel.DataAnnotations;

namespace Battleships.DTOs.Simple;

public class CreateGameDto
{
    [Required(AllowEmptyStrings = false)] public required string PlayerOneName { get; init; }
    [Required(AllowEmptyStrings = false)] public required string PlayerTwoName { get; init; }
    [Range(10, 20)] public required int BoardSize { get; init; }

}