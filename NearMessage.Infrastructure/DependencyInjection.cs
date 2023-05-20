using Microsoft.Extensions.DependencyInjection;
using NearMessage.Application.Abstraction;
using NearMessage.Domain.Repository_Interfaces;
using NearMessage.Infrastructure.Persistence;
using NearMessage.Infrastructure.Repositories;
using NearMessage.Application.Users.Commands.CreateUser;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddScoped<CreateUserAsyncCommandHandler>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}