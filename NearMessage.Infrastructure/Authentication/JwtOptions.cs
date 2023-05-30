namespace NearMessage.Infrastructure.Authentication;

public class JwtOptions
{
    public string Issuer { get; init; }

    public string Audience { get; init; }

    public string SecurityKey { get; init; }
}
