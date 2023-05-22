using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NearMessage.Application.Abstraction;
using NearMessage.Infrastructure.Persistence;

namespace NearMessage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDbContext<NearMessageDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(NearMessageDbContext).Assembly.FullName)));

        services.AddScoped<INearMessageDbContext>(provider => provider.GetRequiredService<NearMessageDbContext>());

        return services;
    }
}
