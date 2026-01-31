using Battleships.Enums;

namespace Battleships.Models.Medium;

public readonly record struct MoveResult(MoveResultEnum MoveResultEnum, bool HasWon);
