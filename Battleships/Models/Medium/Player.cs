using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Battleships.Models;

public class Player
{
    public required string Name { get; init; }
    public required Guid PlayerId { get; init; }

    [SetsRequiredMembers]
    public Player(string name)
    {
        Name = name;
        PlayerId = Guid.NewGuid();
    }
}