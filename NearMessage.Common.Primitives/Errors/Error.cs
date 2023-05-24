namespace NearMessage.Common.Primitives.Errors;

public sealed class Error : IEquatable<Error>
{
    public Error(string message)
    {
        Message = message;
    }

    public static Error None => new(string.Empty);

    public string Message { get; }

    public static bool operator ==(Error a, Error b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }
    
    public bool Equals(Error? other)
        => other is not null && Message == other.Message;

    public static bool operator !=(Error a, Error b) => !(a == b);

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (obj is not Error error)
        {
            return false;
        }

        return Equals(error);
    }

    public override int GetHashCode() => HashCode.Combine(Message);
}
