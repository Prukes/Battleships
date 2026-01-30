using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Battleships.DTOs.Easy;

public class CreateGameDto
{
    [Required(AllowEmptyStrings = false)] public required string PlayerOneName { get; init; }
    [Required(AllowEmptyStrings = false)] public required string PlayerTwoName { get; init; }
    [Range(10, 20)] public required int BoardSize { get; init; }

}