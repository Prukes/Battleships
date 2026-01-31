using Newtonsoft.Json;

namespace Battleships.Models;

public class Player
{
    public required string Name { get; set; }
    [JsonIgnore]
    public Guid ConnectionId { get; }
}