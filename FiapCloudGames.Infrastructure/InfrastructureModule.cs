using FiapCloudGames.Domain.Repositories;
using FiapCloudGames.Infrastructure.Persistence;
using FiapCloudGames.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FiapCloudGames.Infrastructure;

public static class InfrastructureModule {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {
        services
            .AddDbContext(configuration)
            .AddRepositories();

        return services;
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration) {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<FiapCloudGamesDbContext>(options => options.UseSqlServer(connectionString));

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) {
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
