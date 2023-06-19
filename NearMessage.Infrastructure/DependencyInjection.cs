using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Chats;
using NearMessage.Domain.Groups;
using NearMessage.Domain.Messages;
using NearMessage.Domain.UserGroups;
using NearMessage.Domain.Users;
using NearMessage.Domain.UsersInformation;
using NearMessage.Infrastructure.Authentication;
using NearMessage.Infrastructure.Repository;

namespace NearMessage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("FilePath:MessageFilePath").Value ?? "");

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IMessageRepository, MessageRepository>();
        services.AddTransient<IChatRepository, ChatRepository>();
        services.AddTransient<IGroupRepository, GroupRepository>();
        services.AddTransient<IUserGroupRepository, UserGroupRepository>();
        services.AddTransient<IUserInformationRepository, UserInformationRepository>();

        services.AddTransient<JwtOptions>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}