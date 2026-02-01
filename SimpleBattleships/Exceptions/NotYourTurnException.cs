namespace Battleships.Exceptions;

public class NotYourTurnException:Exception
{
    public NotYourTurnException ()
    {}

    public NotYourTurnException (string message) 
        : base(message)
    {}

    public NotYourTurnException (string message, Exception innerException)
        : base (message, innerException)
    {} 
}