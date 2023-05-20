using Microsoft.Extensions.DependencyInjection;
using NearMessage.Application.Abstraction.Messaging;
using NearMessage.Application.Users.Commands.CreateUser;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {

        return services;
    }
}