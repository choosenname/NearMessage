namespace NearMessage.Infrastructure.Authentication;

public class JwtOptions
{
    public string Issuer { get; init; } = String.Empty;

    public string Audience { get; init; } = String.Empty;

    public string SecurityKey { get; init; } = String.Empty;
}
