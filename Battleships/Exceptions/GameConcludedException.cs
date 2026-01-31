namespace Battleships.Exceptions;

public class GameConcludedException:Exception
{
    public GameConcludedException ()
    {}

    public GameConcludedException (string message) 
        : base(message)
    {}

    public GameConcludedException (string message, Exception innerException)
        : base (message, innerException)
    {} 
}