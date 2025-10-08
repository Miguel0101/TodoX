using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using TodoX.Application.Common.Interfaces;
using TodoX.Domain.TodoItems.Interfaces;
using TodoX.Domain.TodoLists.Interfaces;
using TodoX.Domain.Users.Interfaces;
using TodoX.Infrastructure.Data;
using TodoX.Infrastructure.Identity;
using TodoX.Infrastructure.TodoItems.Repositories;
using TodoX.Infrastructure.TodoLists.Repositories;
using TodoX.Infrastructure.Users.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TodoX.Infrastructure;

public static class ConfigureServices
{
    public static void AddInfrastructure(this IServiceCollection services, JwtConfiguration jwtConfiguration, Action<DbContextOptionsBuilder> configureOptions)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services), "Services cannot be null.");

        if (configureOptions == null)
            throw new ArgumentNullException(nameof(configureOptions), "Configure options cannot be null.");

        services.AddDbContext<AppDbContext>(configureOptions);

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfiguration.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtConfiguration.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.PrivateKey)),

                    ValidateLifetime = true
                };
            });

        services.AddAuthorization();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITodoListRepository, TodoListRepository>();
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
    }
}
