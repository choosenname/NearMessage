using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NearMessage.Application.Users.Commands.CreateUser;
using System.Net.NetworkInformation;
using System.Reflection;

namespace NearMessage.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
            this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }
}
