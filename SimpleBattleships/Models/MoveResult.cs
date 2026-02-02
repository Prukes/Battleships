using Battleships.Enums;

namespace Battleships.Models;

public readonly record struct MoveResult(MoveResultEnum MoveResultEnum, bool HasWon);
