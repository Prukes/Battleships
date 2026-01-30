using System.Diagnostics;

namespace Battleships.Exceptions;

[Serializable]
[StackTraceHidden]
public class GameNotFoundException: Exception
{
    
    public GameNotFoundException ()
    {}

    public GameNotFoundException (string message) 
        : base(message)
    {}

    public GameNotFoundException (string message, Exception innerException)
        : base (message, innerException)
    {} 
}