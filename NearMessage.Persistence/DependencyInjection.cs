using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NearMessage.Application.Abstraction;

namespace NearMessage.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddDbContext<NearMessageDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                builder => builder.MigrationsAssembly(typeof(NearMessageDbContext).Assembly.FullName));
            options.UseLazyLoadingProxies(); // Добавьте эту строку для включения отложенной загрузки
        });

        services.AddScoped<INearMessageDbContext>(provider =>
        provider.GetRequiredService<NearMessageDbContext>());

        return services;
    }
}
