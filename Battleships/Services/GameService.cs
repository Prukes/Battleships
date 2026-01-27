using System.Collections.Concurrent;
using Battleships.Models;

namespace Battleships.Services;

public interface IGameService
{
    GameState GetMatch(Guid matchId);
    void CreateMatch(Guid matchId, GameState state);
    Match? JoinQueue(Player player);
    bool FIREEE(Guid matchId, Player player, int posX, int posY);
    
}

public class GameService: IGameService
{
    private readonly ConcurrentQueue<Player> _queue = new ();
    private readonly ConcurrentDictionary<Guid, GameState> _matches = new();
    public GameState GetMatch(Guid matchId)
    {
        throw new NotImplementedException();
    }

    public void CreateMatch(Guid matchId, GameState state)
    {
        throw new NotImplementedException();
    }

    public Match? JoinQueue(Player player)
    {
        throw new NotImplementedException();
    }

    public bool FIREEE(Guid matchId, Player player, int posX, int posY)
    {
        throw new NotImplementedException();
    }
}