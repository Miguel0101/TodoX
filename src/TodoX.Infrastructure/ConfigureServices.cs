using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoX.Infrastructure.Data;

namespace TodoX.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, Action<DbContextOptionsBuilder> configureOptions)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services), "Services cannot be null.");

        if (configureOptions == null)
            throw new ArgumentNullException(nameof(configureOptions), "Configure options cannot be null.");

        services.AddDbContext<AppDbContext>(configureOptions);
    }
}
