using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Battleships.Enums;
using Battleships.Models;

namespace Battleships.DTOs.Simple;

public class GameStateDto
{
    [Required]public required Player MoveBy { get; init; }
    [Required]public required MoveResultEnum MoveResultEnum { get; init; }
    [Required]public required bool HasWon { get; init; }

    [SetsRequiredMembers]
    public GameStateDto(Player moveBy, MoveResultEnum moveResultEnum,bool hasWon)
    {
        MoveBy = moveBy;
        MoveResultEnum = moveResultEnum;
        HasWon = hasWon;
    }
}