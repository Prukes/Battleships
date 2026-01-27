using Battleships.Services;

namespace Battleships.Models;

public class Match(Guid matchId, Player player1,Player player2)
{
    public Guid MatchId { get; set; } = matchId;
    public Player Player1 { get; set; } = player1;
    public Player Player2 { get; set; } = player2;
}