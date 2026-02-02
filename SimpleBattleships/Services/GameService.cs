using System.Collections.Concurrent;
using Battleships.Models;

namespace Battleships.Services;

public interface IGameService
{
    Task<GameState?> GetMatch(Guid matchId);
    void CreateMatch(Guid matchId, GameState state);
    Task<Match?> JoinQueue(int boardSize,Player player);
    bool Fireee(Guid matchId, Player player, int posX, int posY);
    
}

public class GameService: IGameService
{
    private readonly ConcurrentDictionary<int,ConcurrentQueue<Player>>  _queue = new ();
    private readonly ConcurrentDictionary<Guid, GameState> _matches = new();
    public Task<GameState?> GetMatch(Guid matchId)
    {
        throw new NotImplementedException();
    }

    public void CreateMatch(Guid matchId, GameState state)
    {
        throw new NotImplementedException();
    }

    public Task<Match?> JoinQueue(int boardSize,Player player)
    {
        throw new NotImplementedException();
    }

    public bool Fireee(Guid matchId, Player player, int posX, int posY)
    {
        throw new NotImplementedException();
    }
}