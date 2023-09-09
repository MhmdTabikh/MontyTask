using MontyTask.Data.Models;
using MontyTask.Data.Repositories;
using MontyTask.Data;
using MontyTask.Services;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace MontyTask.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("MontyTaskDB"));
        });


        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddSingleton<ITokenHandler, Services.TokenHandler>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISubscriptionService, SubscriptionService>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
