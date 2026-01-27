namespace Battleships.Models;

public class GameState(Player ein, Player zwei, Board einBoard, Board zweiBoard)
{
    public Player PlayerEin { get; set; } = ein;
    public Player PlayerZwei { get; set; } = zwei;
    public Player PlayersTurn { get; set; } = ein;

    public Board PlayerEinBoard { get; set; } = einBoard;
    public Board PlayerZweiBoard { get; set; } = zweiBoard;
}