using Microsoft.Extensions.DependencyInjection;
using NearMessage.Domain.Users;
using NearMessage.Infrastructure.Repository;

namespace NearMessage.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }
}
