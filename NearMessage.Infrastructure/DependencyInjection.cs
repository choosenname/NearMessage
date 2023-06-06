using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Messages;
using NearMessage.Domain.Users;
using NearMessage.Infrastructure.Authentication;
using NearMessage.Infrastructure.Repository;

namespace NearMessage.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("FilePath:MessageFilePath").Value ?? "");

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IMessageRepository, MessageRepository>();

        services.AddTransient<JwtOptions>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}
