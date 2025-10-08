using Microsoft.Extensions.DependencyInjection;
using TodoX.Application.TodoItems.Services;
using TodoX.Application.TodoLists.Services;
using TodoX.Application.Users.Services;

namespace TodoX.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        if (services == null)
            throw new ArgumentNullException(nameof(services));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITodoListService, TodoListService>();
        services.AddScoped<ITodoItemService, TodoItemService>();

        return services;
    }
}