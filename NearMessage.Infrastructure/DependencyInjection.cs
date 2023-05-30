using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Users;
using NearMessage.Infrastructure.Authentication;
using NearMessage.Infrastructure.Repository;

namespace NearMessage.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<JwtOptions>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}
