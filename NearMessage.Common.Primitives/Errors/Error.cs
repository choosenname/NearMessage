namespace NearMessage.Common.Primitives.Errors;

public sealed class Error : IEquatable<Error>
{
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }

    public static Error None => new(string.Empty, string.Empty);

    public string Code { get; }

    public string Message { get; }

    public bool Equals(Error? other) 
        => other is not null && Code == other.Code && Message == other.Message;
}
