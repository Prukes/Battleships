namespace Battleships.DTOs.Realtime;

public class JoinResponseDto
{
    public required Guid PlayerId { get; set; }
    public required string PlayerName { get; set; }
}