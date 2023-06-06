using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NearMessage.Infrastructure.Authentication;
using System.Text;

namespace NearMessage.API.OptionsSetup;

public sealed class JwtBearerOptionsSetup : IPostConfigureOptions<JwtBearerOptions>
{
    private const string ConfigurationSectionName = "Authentication";
    private readonly IConfiguration _configuration;
    private readonly JwtOptions _options;

    public JwtBearerOptionsSetup(IConfiguration configuration, IOptions<JwtOptions> options)
    {
        _configuration = configuration;
        _options = options.Value;
    }

    public void PostConfigure(string? name, JwtBearerOptions options)
    {
        _configuration.GetSection(ConfigurationSectionName).Bind(options);

        options.TokenValidationParameters.ValidIssuer = _options.Issuer;

        options.TokenValidationParameters.ValidAudience = _options.Audience;

        options.TokenValidationParameters.IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecurityKey));
    }
}
