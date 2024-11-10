using Microsoft.Extensions.DependencyInjection;
using NbaTracker.GameManager;

namespace NbaTracker.GameManager;

public static class ServiceRegistration
{
    public static IServiceCollection AddManagerServices(this IServiceCollection services)
    {
        services.AddScoped<IGameManager, GameManager>();
        
        return services;
    }
}