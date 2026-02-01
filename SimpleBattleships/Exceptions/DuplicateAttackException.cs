using System.Diagnostics;

namespace Battleships.Exceptions;

[Serializable]
[StackTraceHidden]
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