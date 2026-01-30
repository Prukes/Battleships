namespace Battleships.Models;

public class Player
{
    public required string Name { get; set; }
    public Guid ConnectionId { get; }
}