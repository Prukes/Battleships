namespace Battleships.Exceptions;

[Serializable]
public class DuplicateAttackException : Exception
{
    public DuplicateAttackException ()
    {}

    public DuplicateAttackException (string message) 
        : base(message)
    {}

    public DuplicateAttackException (string message, Exception innerException)
        : base (message, innerException)
    {} 
}